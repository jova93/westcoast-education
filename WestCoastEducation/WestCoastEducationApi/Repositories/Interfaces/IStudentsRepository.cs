using WestCoastEducationApi.Models;

namespace WestCoastEducationApi.Repositories.Interfaces;

/// <summary>
/// This interface represents a repository that runs CRUD operations against a database.
/// </summary>
public interface IStudentsRepository
{
    Task CreateAsync(Student newStudent);
    Task<List<Student>> GetAsync();
    Task<Student?> GetAsync(string id);
    Task<Student?> GetByEmailAsync(string email);
    Task<List<Course>> GetPurchasedCoursesAsync(string id);
    Task RemoveAsync(string id);
    Task UpdateAsync(string id, Student updatedStudent);
}
