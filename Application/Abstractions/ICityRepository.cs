using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Abstractions;
public interface ICityRepository
{
    Task<City?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<City>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<City>> GetPageAsync(int page, int size, string? q, CancellationToken ct = default);
    Task<int> CountAsync(string? q, CancellationToken ct = default);
    Task AddAsync(City city, CancellationToken ct = default);
    Task UpdateAsync(City cityy, CancellationToken ct = default);
    Task RemoveAsync(City city, CancellationToken ct = default);
}