using MediatR;

namespace ChampionPortal.Application.Commands.Competencies;

public record SetCompetencyHeadCommand(Guid CompetencyId, Guid? EmployeeId) : IRequest;
