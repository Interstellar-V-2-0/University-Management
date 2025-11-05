using UniMgmt.Application.Interfaces;
using UniMgmt.Domain.Entities;
using UniMgmt.Domain.Interfaces;

namespace UniMgmt.Application.Services;

public class StudentService : Service<Student>, IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository) : base(studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Student?> GetStudentByEmailAsync(string email)
    {
        return await _studentRepository.GetByEmailAsync(email);
    }
}