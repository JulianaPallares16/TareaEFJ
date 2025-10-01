using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Abstractions;
public interface IBranchRepository
{
    Task<Branch?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Branch>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Branch>> GetPagedAsync(int page, int size, string? q, CancellationToken ct = default);
    Task<int> CountAsync(string? q, CancellationToken ct = default);
    Task AddAsync(Branch branch, CancellationToken ct = default);
    Task UpdateAsync(Branch branch, CancellationToken ct = default);
    Task RemoveAsync(Branch branch, CancellationToken ct = default);
}
