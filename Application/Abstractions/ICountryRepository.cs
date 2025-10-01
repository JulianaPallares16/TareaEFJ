using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Abstractions;

public interface ICountryRepository
{
    Task<Country?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Country>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Country>> GetPageAsync(int page, int size, string? q, CancellationToken ct = default);
    Task<int> CountAsync(string? q, CancellationToken ct = default);
    Task AddAsync(Country country, CancellationToken ct = default);
    Task UpdateAsync(Country country, CancellationToken ct = default);
    Task RemoveAsync(Country country, CancellationToken ct = default);

}
