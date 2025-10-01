using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs.Region;
using Application.Abstractions;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.Controllers;

[EnableRateLimiting("ipLimiter")]
public class RegionsController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitofwork;

    public RegionsController(IMapper mapper, IUnitOfWork unitofwork)
    {
        _mapper = mapper;
        _unitofwork = unitofwork;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<RegionDto>>> GetAll(CancellationToken ct)
    {
        var regions = await _unitofwork.Regions.GetAllAsync(ct);
        var dto = _mapper.Map<IEnumerable<RegionDto>>(regions);
        return Ok(dto);
    }

    [HttpGet("{id:guid}")]
    [DisableRateLimiting]
    public async Task<ActionResult<RegionDto>> GetById(Guid id, CancellationToken ct)
    {
        var region = await _unitofwork.Regions.GetByIdAsync(id, ct);
        if (region is null) return NotFound();

        return Ok(_mapper.Map<RegionDto>(region));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRegionDto body, CancellationToken ct)
    {

        var region = _mapper.Map<Region>(body);
        await _unitofwork.Regions.AddAsync(region, ct);

        var dto = _mapper.Map<RegionDto>(region);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

}