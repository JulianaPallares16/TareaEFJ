using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs.Country;
using Application.Abstractions;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.Controllers;

[EnableRateLimiting("ipLimiter")]
public class CountriesController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitofwork;

    public CountriesController(IMapper mapper, IUnitOfWork unitofwork)
    {
        _mapper = mapper;
        _unitofwork = unitofwork;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<CountryDto>>> GetAll(CancellationToken ct)
    {
        var countries = await _unitofwork.Countries.GetAllAsync(ct);
        var dto = _mapper.Map<IEnumerable<CountryDto>>(countries);
        return Ok(dto);
    }

    [HttpGet("{id:guid}")]
    [DisableRateLimiting]
    public async Task<ActionResult<CountryDto>> GetById(Guid id, CancellationToken ct)
    {
        var country = await _unitofwork.Countries.GetByIdAsync(id, ct);
        if (country is null) return NotFound();

        return Ok(_mapper.Map<CountryDto>(country));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCountryDto body, CancellationToken ct)
    {

        var country = _mapper.Map<Country>(body);
        await _unitofwork.Countries.AddAsync(country, ct);

        var dto = _mapper.Map<CountryDto>(country);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

}
