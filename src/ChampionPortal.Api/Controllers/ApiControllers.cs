using ChampionPortal.Application.Commands.Activities;
using ChampionPortal.Application.Commands.ActivityTypes;
using ChampionPortal.Application.Commands.Auth;
using ChampionPortal.Application.Commands.Admin;
using ChampionPortal.Application.Commands.Competencies;
using ChampionPortal.Application.DTOs;
using ChampionPortal.Application.Queries.Activities;
using ChampionPortal.Application.Queries.ActivityTypes;
using ChampionPortal.Application.Queries.Admin;
using ChampionPortal.Application.Queries.Dashboard;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChampionPortal.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto request)
    {
        var result = await _mediator.Send(new LoginCommand(request.EmployeeCode));
        return Ok(result);
    }
}

[ApiController]
[Route("api/activity-types")]
[Authorize]
public class ActivityTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ActivityTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ActivityTypeDto>>> GetApproved()
    {
        var result = await _mediator.Send(new GetApprovedActivityTypesQuery());
        return Ok(result);
    }

    [HttpPost("propose")]
    public async Task<ActionResult<ActivityTypeDto>> Propose([FromBody] ProposeActivityTypeRequestDto request)
    {
        var result = await _mediator.Send(new ProposeActivityTypeCommand(
            request.Name,
            request.Description,
            request.SuggestedPoints));
        return Ok(result);
    }

    [HttpGet("pending")]
    public async Task<ActionResult<IReadOnlyList<PendingActivityTypeDto>>> GetPending()
    {
        var result = await _mediator.Send(new GetPendingActivityTypesQuery());
        return Ok(result);
    }

    [HttpPost("{id:guid}/review")]
    public async Task<IActionResult> Review(Guid id, [FromBody] ReviewActivityTypeRequestDto request)
    {
        await _mediator.Send(new ReviewActivityTypeCommand(id, request.Approved, request.Points));
        return NoContent();
    }
}

[ApiController]
[Route("api")]
[Authorize]
public class ActivitiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ActivitiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("activities")]
    public async Task<ActionResult<MyActivityDto>> Submit([FromBody] SubmitActivityRequestDto request)
    {
        var result = await _mediator.Send(new SubmitActivityCommand(
            request.ActivityTypeId,
            request.Title,
            request.Description));
        return Ok(result);
    }

    [HttpGet("me/activities")]
    public async Task<ActionResult<MyActivitiesResponseDto>> GetMine()
    {
        var result = await _mediator.Send(new GetMyActivitiesQuery());
        return Ok(result);
    }

    [HttpGet("employees/{employeeId:guid}/activities")]
    public async Task<ActionResult<EmployeeActivitiesResponseDto>> GetEmployeeActivities(Guid employeeId)
    {
        var result = await _mediator.Send(new GetEmployeeActivitiesQuery(employeeId));
        return Ok(result);
    }

    [HttpPost("activities/{id:guid}/endorse")]
    public async Task<IActionResult> Endorse(Guid id, [FromBody] EndorseActivityRequestDto request)
    {
        await _mediator.Send(new EndorseActivityCommand(id, request.Comment));
        return NoContent();
    }
}

[ApiController]
[Route("api/dashboard")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly IMediator _mediator;

    public DashboardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<DashboardDto>> Get(
        [FromQuery] string? search,
        [FromQuery] DateTime? rangeStart,
        [FromQuery] DateTime? rangeEnd)
    {
        var result = await _mediator.Send(new GetDashboardQuery(search, rangeStart, rangeEnd));
        return Ok(result);
    }
}

[ApiController]
[Route("api/admin/employees")]
[Authorize]
public class AdminEmployeesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminEmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AdminEmployeeListItemDto>>> List(
        [FromQuery] bool includeInactive = false)
    {
        var result = await _mediator.Send(new GetAdminEmployeesQuery(includeInactive));
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AdminEmployeeDetailDto>> Get(Guid id)
    {
        var result = await _mediator.Send(new GetAdminEmployeeByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<AdminEmployeeDetailDto>> Create([FromBody] CreateAdminEmployeeRequestDto request)
    {
        var result = await _mediator.Send(new CreateAdminEmployeeCommand(
            request.EmployeeCode,
            request.Name,
            request.Email,
            request.CompetencyId,
            request.IsReviewer,
            request.IsAdmin));
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<AdminEmployeeDetailDto>> Update(
        Guid id,
        [FromBody] UpdateAdminEmployeeRequestDto request)
    {
        var result = await _mediator.Send(new UpdateAdminEmployeeCommand(
            id,
            request.Name,
            request.Email,
            request.CompetencyId,
            request.IsReviewer,
            request.IsAdmin,
            request.IsActive));
        return Ok(result);
    }

    [HttpPost("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        await _mediator.Send(new DeactivateAdminEmployeeCommand(id));
        return NoContent();
    }
}

[ApiController]
[Route("api/admin/competencies")]
[Authorize]
public class AdminCompetenciesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminCompetenciesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AdminCompetencyListItemDto>>> List(
        [FromQuery] bool includeInactive = false)
    {
        var result = await _mediator.Send(new GetAdminCompetenciesQuery(includeInactive));
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<AdminCompetencyListItemDto>> Create(
        [FromBody] CreateAdminCompetencyRequestDto request)
    {
        var result = await _mediator.Send(new CreateAdminCompetencyCommand(
            request.Code,
            request.Name,
            request.Description,
            request.SortOrder));
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<AdminCompetencyListItemDto>> Update(
        Guid id,
        [FromBody] UpdateAdminCompetencyRequestDto request)
    {
        var result = await _mediator.Send(new UpdateAdminCompetencyCommand(
            id,
            request.Name,
            request.Description,
            request.SortOrder,
            request.IsActive));
        return Ok(result);
    }

    [HttpPut("{id:guid}/head")]
    public async Task<IActionResult> SetHead(Guid id, [FromBody] SetCompetencyHeadRequestDto request)
    {
        await _mediator.Send(new SetCompetencyHeadCommand(id, request.EmployeeId));
        return NoContent();
    }
}
