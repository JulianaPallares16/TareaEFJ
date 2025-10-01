using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
public sealed class CountryRepository(AppDbContext db) : ICountryRepository
{
    public Task<Country?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => db.Countries
            .AsNoTracking()
            .Include(c => c.Regions)
            .FirstOrDefaultAsync(c => c.Id == id, ct);

    public async Task<IReadOnlyList<Country>> GetAllAsync(CancellationToken ct = default)
    {
        return await db.Countries
            .AsNoTracking()
            .Include(c => c.Regions)
            .OrderBy(c => c.Name)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Country>> GetPagedAsync(int page, int size, string? q, CancellationToken ct = default)
    {
        var query = db.Countries.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim().ToUpper();
            query = query.Where(c => c.Name!.ToUpper().Contains(term));
        }

        return await query
            .OrderBy(c => c.Name)
            .Skip((page - 1) * size)
            .Take(size)
            .Include(c => c.Regions)
            .ToListAsync(ct);
    }

    public Task<int> CountAsync(string? q, CancellationToken ct = default)
    {
        var query = db.Countries.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim().ToUpper();
            query = query.Where(c => c.Name!.ToUpper().Contains(term));
        }

        return query.CountAsync(ct);
    }

    public async Task AddAsync(Country country, CancellationToken ct = default)
    {
        db.Countries.Add(country);
        // await db.SaveChangesAsync(ct);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(Country country, CancellationToken ct = default)
    {
        db.Countries.Update(country);
        // await db.SaveChangesAsync(ct);
        await Task.CompletedTask;
    }

    public async Task RemoveAsync(Country country, CancellationToken ct = default)
    {
        db.Countries.Remove(country);
        await db.SaveChangesAsync(ct);
    }
}