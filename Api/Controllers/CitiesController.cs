using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs.City;
using Application.Abstractions;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.Controllers;

[EnableRateLimiting("ipLimiter")]
public class CitiesController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitofwork;

    public CitiesController(IMapper mapper, IUnitOfWork unitofwork)
    {
        _mapper = mapper;
        _unitofwork = unitofwork;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<CityDto>>> GetAll(CancellationToken ct)
    {
        var cities = await _unitofwork.Cities.GetAllAsync(ct);
        var dto = _mapper.Map<IEnumerable<CityDto>>(cities);
        return Ok(dto);
    }

    [HttpGet("{id:guid}")]
    [DisableRateLimiting]
    public async Task<ActionResult<CityDto>> GetById(Guid id, CancellationToken ct)
    {
        var city = await _unitofwork.Cities.GetByIdAsync(id, ct);
        if (city is null) return NotFound();

        return Ok(_mapper.Map<CityDto>(city));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCityDto body, CancellationToken ct)
    {

        var city = _mapper.Map<City>(body);
        await _unitofwork.Cities.AddAsync(city, ct);

        var dto = _mapper.Map<CityDto>(city);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

}