using AutoMapper;
using FluentResults;
using MediatR;
using Swats.Data.Repository;
using Swats.Model.Commands;
using Swats.Model.Domain;
using System.Text.Json;

namespace Swats.Infrastructure.Features.Teams.CreateTeam;

public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, Result<string>>
{
    private readonly IManageRepository _manageRepository;
    private readonly IMapper _mapper;

    public CreateTeamCommandHandler(IManageRepository manageRepository, IMapper mapper)
    {
        _manageRepository = manageRepository;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = _mapper.Map<CreateTeamCommand, Team>(request);

        var auditLog = new DbAuditLog
        {
            Target = team.Id,
            ActionName = "team.create",
            Description = "added team",
            ObjectName = "team",
            ObjectData = JsonSerializer.Serialize(team),
            CreatedBy = request.CreatedBy
        };

        var rst = await _manageRepository.CreateTeam(team, auditLog, cancellationToken);
        return rst > 0 ? Result.Ok(team.Id) : Result.Fail<string>("Not able to create now!");
    }
}