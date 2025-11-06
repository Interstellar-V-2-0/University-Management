using Microsoft.AspNetCore.Mvc;
using UniMgmt.Application.Interfaces;
using UniMgmt.Api.Dtos.Course;
using AutoMapper;
using UniMgmt.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;
    private readonly IMapper _mapper;

    public CoursesController(ICourseService courseService, IMapper mapper)
    {
        _courseService = courseService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
    {
        var courses = await _courseService.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<CourseDto>>(courses));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CourseDto>> GetCourse(int id)
    {
        var course = await _courseService.GetByIdAsync(id);
        if (course == null) return NotFound();
        return Ok(_mapper.Map<CourseDto>(course));
    }

    [HttpPost]
    public async Task<ActionResult<CourseDto>> CreateCourse(CreateCourseDto dto)
    {
        var course = _mapper.Map<Course>(dto);
        await _courseService.CreateAsync(course);
        return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, _mapper.Map<CourseDto>(course));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(int id, UpdateCourseDto dto)
    {
        var course = await _courseService.GetByIdAsync(id);
        if (course == null) return NotFound();
        _mapper.Map(dto, course);
        await _courseService.UpdateAsync(course);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var course = await _courseService.GetByIdAsync(id);
        if (course == null) return NotFound();
        await _courseService.DeleteAsync(id);
        return NoContent();
    }
}