using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
public class CompanyRepository : ICompanyRepository
{
    private readonly AppDbContext _context;

    public CompanyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Company?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Companies
            .Include(c => c.City)
            .ThenInclude(r => r.Region)
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task<IReadOnlyList<Company>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Companies
            .Include(c => c.City)
            .ThenInclude(r => r.Region)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Company>> GetByCityIdAsync(Guid cityId, CancellationToken ct = default)
    {
        return await _context.Companies
            .Where(c => c.CityId == cityId)
            .Include(c => c.City)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Company>> GetPagedAsync(int page, int size, string? q, CancellationToken ct = default)
    {
        var query = _context.Companies.AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            query = query.Where(c => c.Name!.Contains(q));
        }

        return await query
            .Include(c => c.City)
            .Skip((page - 1) * size)
            .Take(size)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<int> CountAsync(string? q, CancellationToken ct = default)
    {
        var query = _context.Companies.AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            query = query.Where(c => c.Name!.Contains(q));
        }

        return await query.CountAsync(ct);
    }

    public async Task AddAsync(Company company, CancellationToken ct = default)
    {
        await _context.Companies.AddAsync(company, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Company company, CancellationToken ct = default)
    {
        _context.Companies.Update(company);
        await _context.SaveChangesAsync(ct);
    }

    public async Task RemoveAsync(Company company, CancellationToken ct = default)
    {
        _context.Companies.Remove(company);
        await _context.SaveChangesAsync(ct);
    }
}