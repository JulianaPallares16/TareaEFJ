using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs.Branch;
using Application.Abstractions;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.Controllers;

[EnableRateLimiting("ipLimiter")]
public class BranchesController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitofwork;

    public BranchesController(IMapper mapper, IUnitOfWork unitofwork)
    {
        _mapper = mapper;
        _unitofwork = unitofwork;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<BranchDto>>> GetAll(CancellationToken ct)
    {
        var branches = await _unitofwork.Branches.GetAllAsync(ct);
        var dto = _mapper.Map<IEnumerable<BranchDto>>(branches);
        return Ok(dto);
    }

    [HttpGet("{id:guid}")]
    [DisableRateLimiting]
    public async Task<ActionResult<BranchDto>> GetById(Guid id, CancellationToken ct)
    {
        var branch = await _unitofwork.Branches.GetByIdAsync(id, ct);
        if (branch is null) return NotFound();

        return Ok(_mapper.Map<BranchDto>(branch));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBranchDto body, CancellationToken ct)
    {

        var branch = _mapper.Map<Branch>(body);
        await _unitofwork.Branches.AddAsync(branch, ct);

        var dto = _mapper.Map<BranchDto>(branch);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

}
