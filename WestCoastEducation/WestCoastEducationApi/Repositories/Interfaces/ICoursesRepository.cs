using WestCoastEducationApi.Models;

namespace WestCoastEducationApi.Repositories.Interfaces;

/// <summary>
/// This interface represents a repository that runs CRUD operations against a database.
/// </summary>
public interface ICoursesRepository
{
    Task CreateAsync(Course newCourse);
    Task<List<Course>> GetAsync();
    Task<Course?> GetAsync(string id);
    Task<Course?> GetByNumberAsync(string number);
    Task<List<Student>> GetParticipantsAsync(string id);
    Task RemoveAsync(string id);
    Task UpdateAsync(string id, Course updatedCourse);
}
