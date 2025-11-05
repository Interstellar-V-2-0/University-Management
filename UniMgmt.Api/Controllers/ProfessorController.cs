using Microsoft.AspNetCore.Mvc;
using UniMgmt.Application.Interfaces;
using UniMgmt.Api.Dtos.Professor;
using AutoMapper;
using UniMgmt.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class ProfessorsController : ControllerBase
{
    private readonly IProfessorService _professorService;
    private readonly IMapper _mapper;

    public ProfessorsController(IProfessorService professorService, IMapper mapper)
    {
        _professorService = professorService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProfessorDto>>> GetProfessors()
    {
        var professors = await _professorService.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professors));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProfessorDto>> GetProfessor(int id)
    {
        var professor = await _professorService.GetByIdAsync(id);
        if (professor == null) return NotFound();
        return Ok(_mapper.Map<ProfessorDto>(professor));
    }

    [HttpPost]
    public async Task<ActionResult<ProfessorDto>> CreateProfessor(CreateProfessorDto dto)
    {
        var professor = _mapper.Map<Professor>(dto);
        await _professorService.CreateAsync(professor);
        return CreatedAtAction(nameof(GetProfessor), new { id = professor.Id }, _mapper.Map<ProfessorDto>(professor));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProfessor(int id, UpdateProfessorDto dto)
    {
        var professor = await _professorService.GetByIdAsync(id);
        if (professor == null) return NotFound();
        _mapper.Map(dto, professor);
        await _professorService.UpdateAsync(professor);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProfessor(int id)
    {
        var professor = await _professorService.GetByIdAsync(id);
        if (professor == null) return NotFound();
        await _professorService.DeleteAsync(id);
        return NoContent();
    }
}
