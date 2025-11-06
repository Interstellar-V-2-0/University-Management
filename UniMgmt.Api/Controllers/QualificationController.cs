using Microsoft.AspNetCore.Mvc;
using UniMgmt.Application.Interfaces;
using UniMgmt.Api.Dtos.Qualification;
using AutoMapper;
using UniMgmt.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class QualificationsController : ControllerBase
{
    private readonly IQualificationService _qualificationService;
    private readonly IMapper _mapper;

    public QualificationsController(IQualificationService qualificationService, IMapper mapper)
    {
        _qualificationService = qualificationService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<QualificationDto>>> GetQualifications()
    {
        var qualifications = await _qualificationService.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<QualificationDto>>(qualifications));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<QualificationDto>> GetQualification(int id)
    {
        var qualification = await _qualificationService.GetByIdAsync(id);
        if (qualification == null) return NotFound();
        return Ok(_mapper.Map<QualificationDto>(qualification));
    }

    [HttpPost]
    public async Task<ActionResult<QualificationDto>> CreateQualification(CreateQualificationDto dto)
    {
        var qualification = _mapper.Map<Qualification>(dto);
        await _qualificationService.CreateAsync(qualification);
        return CreatedAtAction(nameof(GetQualification), new { id = qualification.Id }, _mapper.Map<QualificationDto>(qualification));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQualification(int id, UpdateQualificationDto dto)
    {
        var qualification = await _qualificationService.GetByIdAsync(id);
        if (qualification == null) return NotFound();
        _mapper.Map(dto, qualification);
        await _qualificationService.UpdateAsync(qualification);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQualification(int id)
    {
        var qualification = await _qualificationService.GetByIdAsync(id);
        if (qualification == null) return NotFound();
        await _qualificationService.DeleteAsync(id);
        return NoContent();
    }
}
