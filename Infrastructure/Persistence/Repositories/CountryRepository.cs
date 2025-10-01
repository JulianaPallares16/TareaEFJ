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
        => db.Countries.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, ct);

    /*public Task<Country?> GetBySkuAsync(Sku sku, CancellationToken ct = default)
        => db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Sku.Value == sku.Value, ct);*/

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

    /*public Task<bool> ExistsSkuAsync(Sku sku, CancellationToken ct = default)
        => db.Products.AsNoTracking().AnyAsync(p => p.Sku.Value == sku.Value, ct);*/

    public async Task<IReadOnlyList<Country>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default)
    {
        var query = db.Countries.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToUpper();
            query = query.Where(c =>
                c.Name.ToUpper().Contains(term) /*||
                c.Sku.Value.ToUpper().Contains(term)*/);
        }

        return await query
            .OrderByDescending(c => c.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
    }

    public Task<int> CountAsync(string? search = null, CancellationToken ct = default)
    {
        var query = db.Countries.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToUpper();
            query = query.Where(c =>
                c.Name.ToUpper().Contains(term) /*||
                p.Sku.Value.ToUpper().Contains(term)*/);
        }
        return query.CountAsync(ct);
    }
    public async Task<IReadOnlyList<Country>> GetAllAsync(CancellationToken ct = default)
    {
        return await db.Countries
            .AsNoTracking()
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(ct);
    }
}