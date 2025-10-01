using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Branches;
public sealed class CreateBranchHandler(IBranchRepository repo) : IRequestHandler<CreateBranch, Guid>
{
    public async Task<Guid> Handle(CreateBranch req, CancellationToken ct)
    {
        var branch = new Branch(req.NumeroComercial, req.Address, req.Email, req.ContactName, req.Phone);
        await repo.AddAsync(branch, ct);
        return branch.Id;
    }
}