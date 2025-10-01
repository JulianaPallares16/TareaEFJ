using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Cities;
public sealed class CreateCityHandler(ICityRepository repo) : IRequestHandler<CreateCity, Guid>
{
    public async Task<Guid> Handle(CreateCity req, CancellationToken ct)
    {
        var city = new City(req.Name);
        await repo.AddAsync(city, ct);
        return city.Id;
    }
}