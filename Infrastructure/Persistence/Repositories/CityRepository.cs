using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
public class CityRepository : ICityRepository
{
    private readonly AppDbContext _context;

    public CityRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<City?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Cities
            .Include(c => c.Region)
            .ThenInclude(r => r.Country)
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task<IReadOnlyList<City>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Cities
            .Include(c => c.Region)
            .ThenInclude(r => r.Country)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<City>> GetByRegionIdAsync(Guid regionId, CancellationToken ct = default)
    {
        return await _context.Cities
            .Where(c => c.RegionId == regionId)
            .Include(c => c.Region)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<City>> GetPagedAsync(int page, int size, string? q, CancellationToken ct = default)
    {
        var query = _context.Cities.AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            query = query.Where(c => c.Name!.Contains(q));
        }

        return await query
            .Include(c => c.Region)
            .OrderBy(c => c.Name)
            .Skip((page - 1) * size)
            .Take(size)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<int> CountAsync(string? q, CancellationToken ct = default)
    {
        var query = _context.Cities.AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            query = query.Where(c => c.Name!.Contains(q));
        }

        return await query.CountAsync(ct);
    }

    public async Task AddAsync(City city, CancellationToken ct = default)
    {
        await _context.Cities.AddAsync(city, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(City city, CancellationToken ct = default)
    {
        _context.Cities.Update(city);
        await _context.SaveChangesAsync(ct);
    }

    public async Task RemoveAsync(City city, CancellationToken ct = default)
    {
        _context.Cities.Remove(city);
        await _context.SaveChangesAsync(ct);
    }
}