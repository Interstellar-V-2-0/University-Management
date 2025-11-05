using Microsoft.AspNetCore.Mvc;
using UniMgmt.Application.Interfaces;
using UniMgmt.Api.Dtos.Section;
using AutoMapper;
using UniMgmt.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class SectionController : ControllerBase
{
    private readonly ISectionService _sectionService;
    private readonly IMapper _mapper;

    public SectionController(ISectionService sectionService, IMapper mapper)
    {
        _sectionService = sectionService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SectionDto>>> GetSections()
    {
        var sections = await _sectionService.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<SectionDto>>(sections));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SectionDto>> GetSection(int id)
    {
        var section = await _sectionService.GetByIdAsync(id);
        if (section == null) return NotFound();
        return Ok(_mapper.Map<SectionDto>(section));
    }

    [HttpPost]
    public async Task<ActionResult<SectionDto>> CreateSection(CreateSectionDto dto)
    {
        var section = _mapper.Map<Section>(dto);
        await _sectionService.CreateAsync(section);
        return CreatedAtAction(nameof(GetSection), new { id = section.Id }, _mapper.Map<SectionDto>(section));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSection(int id, UpdateSectionDto dto)
    {
        var section = await _sectionService.GetByIdAsync(id);
        if (section == null) return NotFound();
        _mapper.Map(dto, section);
        await _sectionService.UpdateAsync(section);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSection(int id)
    {
        var section = await _sectionService.GetByIdAsync(id);
        if (section == null) return NotFound();
        await _sectionService.DeleteAsync(id);
        return NoContent();
    }
}