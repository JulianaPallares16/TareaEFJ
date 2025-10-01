using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Abstractions;
public interface IRegionRepository
{
    Task<Region?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Region>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Region>> GetPagedAsync(int page, int size, string? q, CancellationToken ct = default);
    Task<int> CountAsync(string? q, CancellationToken ct = default);
    Task AddAsync(Region region, CancellationToken ct = default);
    Task UpdateAsync(Region region, CancellationToken ct = default);
    Task RemoveAsync(Region region, CancellationToken ct = default);
}
