using WestCoastEducationApi.Models;

namespace WestCoastEducationApi.Services.Interfaces;

public interface IStudentsService
{
    Task CreateAsync(Student newStudent);
    Task<List<Student>> GetAsync();
    Task<Student?> GetAsync(string id);
    Task<Student?> GetByEmailAsync(string email);
    Task<List<Course>> GetPurchasedCoursesAsync(string id);
    Task RemoveAsync(string id);
    Task UpdateAsync(string id, Student updatedStudent);
}
