using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Regions;
public sealed class CreateRegionHandler(IRegionRepository repo) : IRequestHandler<CreateRegion, Guid>
{
    public async Task<Guid> Handle(CreateRegion req, CancellationToken ct)
    {
        var region = new Region(req.Name);
        await repo.AddAsync(region, ct);
        return region.Id;
    }
}
