using WestCoastEducationApi.Models;
using WestCoastEducationApi.Repositories.Interfaces;
using WestCoastEducationApi.Services.Interfaces;

namespace WestCoastEducationApi.Services;

public class StudentsService : IStudentsService
{
    private readonly IStudentsRepository _studentsRepository;

    public StudentsService(IStudentsRepository studentsRepository)
    {
        _studentsRepository = studentsRepository;
    }

    // Get all students.
    public async Task<List<Student>> GetAsync() => await _studentsRepository.GetAsync();

    // Get a specific student based on Id.
    public async Task<Student?> GetAsync(string id) => await _studentsRepository.GetAsync(id);

    // Get a specific student based on Email.
    public async Task<Student?> GetByEmailAsync(string email) => await _studentsRepository.GetByEmailAsync(email);

    // Create a new student and add it to the Students collection.
    public async Task CreateAsync(Student newStudent) => await _studentsRepository.CreateAsync(newStudent);

    // Update a specific student based on Id.
    public async Task UpdateAsync(string id, Student updatedStudent) => await _studentsRepository.UpdateAsync(id, updatedStudent);

    // Remove a specific student based on Id.
    public async Task RemoveAsync(string id) => await _studentsRepository.RemoveAsync(id);

    // Get all the courses that the student has purchased.
    public async Task<List<Course>> GetPurchasedCoursesAsync(string id) => await _studentsRepository.GetPurchasedCoursesAsync(id);
}
