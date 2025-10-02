using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class RegionRepository(AppDbContext db) : IRegionRepository
{
    public Task<Region?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => db.Regions
            .AsNoTracking()
            .Include(r => r.Cities)
            .FirstOrDefaultAsync(r => r.Id == id, ct);

    public async Task<IReadOnlyList<Region>> GetAllAsync(CancellationToken ct = default)
    {
        return await db.Regions
            .AsNoTracking()
            .Include(r => r.Cities)
            .OrderBy(r => r.Name)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Region>> GetByCountryIdAsync(Guid countryId, CancellationToken ct = default)
    {
        return await db.Regions
            .AsNoTracking()
            .Where(r => r.CountryId == countryId)
            .Include(r => r.Cities)
            .OrderBy(r => r.Name)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Region>> GetPagedAsync(int page, int size, string? q, CancellationToken ct = default)
    {
        var query = db.Regions.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim().ToUpper();
            query = query.Where(r => r.Name!.ToUpper().Contains(term));
        }

        return await query
            .OrderBy(r => r.Name)
            .Skip((page - 1) * size)
            .Take(size)
            .Include(r => r.Cities)
            .ToListAsync(ct);
    }

    public Task<int> CountAsync(string? q, CancellationToken ct = default)
    {
        var query = db.Regions.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim().ToUpper();
            query = query.Where(r => r.Name!.ToUpper().Contains(term));
        }

        return query.CountAsync(ct);
    }

    public async Task AddAsync(Region region, CancellationToken ct = default)
    {
        db.Regions.Add(region);
        // await db.SaveChangesAsync(ct);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(Region region, CancellationToken ct = default)
    {
        db.Regions.Update(region);
        // await db.SaveChangesAsync(ct);
        await Task.CompletedTask;
    }

    public async Task RemoveAsync(Region region, CancellationToken ct = default)
    {
        db.Regions.Remove(region);
        await db.SaveChangesAsync(ct);
    }
}