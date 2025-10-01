using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Countries;
public sealed class CreateCountryHandler(ICountryRepository repo) : IRequestHandler<CreateCountry, Guid>
{
    public async Task<Guid> Handle(CreateCountry req, CancellationToken ct)
    {
        var country = new Country(req.Name);
        await repo.AddAsync(country, ct);
        return country.Id;
    }
}
