using Microsoft.AspNetCore.Mvc;
using UniMgmt.Application.Interfaces;
using UniMgmt.Api.Dtos.Inscription;
using AutoMapper;
using UniMgmt.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class InscriptionsController : ControllerBase
{
    private readonly IInscriptionService _inscriptionService;
    private readonly IMapper _mapper;

    public InscriptionsController(IInscriptionService inscriptionService, IMapper mapper)
    {
        _inscriptionService = inscriptionService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InscriptionDto>>> GetInscriptions()
    {
        var inscriptions = await _inscriptionService.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<InscriptionDto>>(inscriptions));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InscriptionDto>> GetInscription(int id)
    {
        var inscription = await _inscriptionService.GetByIdAsync(id);
        if (inscription == null) return NotFound();
        return Ok(_mapper.Map<InscriptionDto>(inscription));
    }

    [HttpPost]
    public async Task<ActionResult<InscriptionDto>> CreateInscription(CreateInscriptionDto dto)
    {
        var inscription = _mapper.Map<Inscription>(dto);
        await _inscriptionService.CreateAsync(inscription);
        return CreatedAtAction(nameof(GetInscription), new { id = inscription.Id }, _mapper.Map<InscriptionDto>(inscription));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInscription(int id)
    {
        var inscription = await _inscriptionService.GetByIdAsync(id);
        if (inscription == null) return NotFound();
        await _inscriptionService.DeleteAsync(id);
        return NoContent();
    }
}