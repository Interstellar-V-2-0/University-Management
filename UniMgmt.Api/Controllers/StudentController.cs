using Microsoft.AspNetCore.Mvc;
using UniMgmt.Application.Interfaces;
using UniMgmt.Api.Dtos.Student;
using AutoMapper;
using UniMgmt.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly IMapper _mapper;

    public StudentsController(IStudentService studentService, IMapper mapper)
    {
        _studentService = studentService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
    {
        var students = await _studentService.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<StudentDto>>(students));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StudentDto>> GetStudent(int id)
    {
        var student = await _studentService.GetByIdAsync(id);
        if (student == null) return NotFound();
        return Ok(_mapper.Map<StudentDto>(student));
    }

    [HttpPost]
    public async Task<ActionResult<StudentDto>> CreateStudent(CreateStudentDto dto)
    {
        var student = _mapper.Map<Student>(dto);
        await _studentService.CreateAsync(student);
        return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, _mapper.Map<StudentDto>(student));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(int id, UpdateStudentDto dto)
    {
        var student = await _studentService.GetByIdAsync(id);
        if (student == null) return NotFound();
        _mapper.Map(dto, student);
        await _studentService.UpdateAsync(student);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var student = await _studentService.GetByIdAsync(id);
        if (student == null) return NotFound();
        await _studentService.DeleteAsync(id);
        return NoContent();
    }

}