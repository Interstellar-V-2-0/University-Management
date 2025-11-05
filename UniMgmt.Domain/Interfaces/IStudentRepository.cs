using UniMgmt.Domain.Entities;

namespace UniMgmt.Domain.Interfaces;

public interface IStudentRepository : IRepository<Student>
{
    Task<Student?> GetByEmailAsync(string email);
}