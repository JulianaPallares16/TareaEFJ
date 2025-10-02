using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
public class BranchRepository : IBranchRepository
{
    private readonly AppDbContext _context;

    public BranchRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Branch?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Branches
            .Include(b => b.City)
                .ThenInclude(c => c.Region)
                .ThenInclude(r => r.Country)
            .Include(b => b.Company)
            .FirstOrDefaultAsync(b => b.Id == id, ct);
    }

    public async Task<IReadOnlyList<Branch>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Branches
            .Include(b => b.City)
                .ThenInclude(c => c.Region)
            .Include(b => b.Company)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Branch>> GetByCityIdAsync(Guid cityId, CancellationToken ct = default)
    {
        return await _context.Branches
            .Where(b => b.CityId == cityId)
            .Include(b => b.City)
            .Include(b => b.Company)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Branch>> GetByCompanyIdAsync(Guid companyId, CancellationToken ct = default)
    {
        return await _context.Branches
            .Where(b => b.CompanyId == companyId)
            .Include(b => b.City)
            .Include(b => b.Company)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Branch>> GetPagedAsync(int page, int size, string? q, CancellationToken ct = default)
    {
        var query = _context.Branches.AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            query = query.Where(b =>
                b.Address!.Contains(q) ||
                b.ContactName!.Contains(q) ||
                b.Email!.Contains(q));
        }

        return await query
            .Include(b => b.City)
            .Include(b => b.Company)
            .OrderBy(b => b.NumeroComercial)
            .Skip((page - 1) * size)
            .Take(size)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<int> CountAsync(string? q, CancellationToken ct = default)
    {
        var query = _context.Branches.AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            query = query.Where(b =>
                b.Address!.Contains(q) ||
                b.ContactName!.Contains(q) ||
                b.Email!.Contains(q));
        }

        return await query.CountAsync(ct);
    }

    public async Task AddAsync(Branch branch, CancellationToken ct = default)
    {
        await _context.Branches.AddAsync(branch, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Branch branch, CancellationToken ct = default)
    {
        _context.Branches.Update(branch);
        await _context.SaveChangesAsync(ct);
    }

    public async Task RemoveAsync(Branch branch, CancellationToken ct = default)
    {
        _context.Branches.Remove(branch);
        await _context.SaveChangesAsync(ct);
    }
}
