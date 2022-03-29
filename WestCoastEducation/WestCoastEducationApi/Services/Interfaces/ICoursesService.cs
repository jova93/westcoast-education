using WestCoastEducationApi.Models;

namespace WestCoastEducationApi.Services.Interfaces;

public interface ICoursesService
{
    Task CreateAsync(Course newCourse);
    Task<List<Course>> GetAsync();
    Task<Course?> GetAsync(string id);
    Task<Course?> GetByNumberAsync(string number);
    Task<List<Student>> GetParticipantsAsync(string id);
    Task RemoveAsync(string id);
    Task UpdateAsync(string id, Course updatedCourse);
}
