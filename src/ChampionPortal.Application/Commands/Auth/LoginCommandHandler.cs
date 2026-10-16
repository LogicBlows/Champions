using ChampionPortal.Application.Commands.Auth;
using ChampionPortal.Application.DTOs;
using ChampionPortal.Application.Interfaces;
using MediatR;

namespace ChampionPortal.Application.Commands.Auth;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginCommandHandler(
        IEmployeeRepository employeeRepository,
        IJwtTokenService jwtTokenService)
    {
        _employeeRepository = employeeRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByEmployeeCodeAsync(request.EmployeeCode);
        if (employee is null || !employee.IsActive)
        {
            throw new UnauthorizedAccessException("Invalid employee code.");
        }

        var token = _jwtTokenService.GenerateToken(employee);
        return new LoginResponseDto(token, employee.Id, employee.Name, employee.IsReviewer, employee.IsAdmin);
    }
}
