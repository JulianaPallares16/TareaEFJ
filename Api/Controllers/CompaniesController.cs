using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs.Company;
using Application.Abstractions;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.Controllers;

[EnableRateLimiting("ipLimiter")]
public class CompaniesController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitofwork;

    public CompaniesController(IMapper mapper, IUnitOfWork unitofwork)
    {
        _mapper = mapper;
        _unitofwork = unitofwork;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetAll(CancellationToken ct)
    {
        var companies = await _unitofwork.Companies.GetAllAsync(ct);
        var dto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
        return Ok(dto);
    }

    [HttpGet("{id:guid}")]
    [DisableRateLimiting]
    public async Task<ActionResult<CompanyDto>> GetById(Guid id, CancellationToken ct)
    {
        var company = await _unitofwork.Companies.GetByIdAsync(id, ct);
        if (company is null) return NotFound();

        return Ok(_mapper.Map<CompanyDto>(company));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCompanyDto body, CancellationToken ct)
    {

        var country = _mapper.Map<Company>(body);
        await _unitofwork.Companies.AddAsync(country, ct);

        var dto = _mapper.Map<CompanyDto>(country);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

}
