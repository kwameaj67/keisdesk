﻿using AutoMapper;
using FluentResults;
using MediatR;
using Swats.Data.Repository;
using Swats.Model;
using Swats.Model.Commands;
using Swats.Model.Domain;
using System.Text.Json;

namespace Swats.Infrastructure.Features.Department.CreateDepartment;

public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Result<Guid>>
{
    private readonly IManageRepository _manageRepository;
    private readonly IMapper _mapper;

    public CreateDepartmentCommandHandler(IManageRepository manageRepository, IMapper mapper)
    {
        _manageRepository = manageRepository;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = _mapper.Map<CreateDepartmentCommand, Model.Domain.Department>(request);

        var auditLog = new DbAuditLog
        {
            Target = department.Id,
            ActionName = "department.create",
            Description = "added department hour",
            ObjectName = "department",
            ObjectData = JsonSerializer.Serialize(department),
            CreatedBy = request.CreatedBy,
        };

        var rst = await _manageRepository.CreateDepartment(department, auditLog, cancellationToken);
        return rst > 0 ? Result.Ok(department.Id.ToGuid()) : Result.Fail<Guid>("Not able to create now!");
    }
}
