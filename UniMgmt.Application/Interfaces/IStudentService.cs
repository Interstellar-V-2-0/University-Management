using UniMgmt.Domain.Entities;

namespace UniMgmt.Application.Interfaces;

public interface IStudentService : IService<Student>
{
    Task<Student?> GetStudentByEmailAsync(string email);
}