using WestCoastEducationApi.Models;
using WestCoastEducationApi.Repositories.Interfaces;
using WestCoastEducationApi.Services.Interfaces;

namespace WestCoastEducationApi.Services;

public class CoursesService : ICoursesService
{
    private readonly ICoursesRepository _coursesRepository;

    public CoursesService(ICoursesRepository coursesRepository)
    {
        _coursesRepository = coursesRepository;
    }

    // Get all courses.
    public async Task<List<Course>> GetAsync() => await _coursesRepository.GetAsync();

    // Get a specific course based on Id.
    public async Task<Course?> GetAsync(string id) => await _coursesRepository.GetAsync(id);

    // Get a specific course based on Number.
    public async Task<Course?> GetByNumberAsync(string number) => await _coursesRepository.GetByNumberAsync(number);

    // Create a new course and add it to the Courses collection.
    public async Task CreateAsync(Course newCourse) => await _coursesRepository.CreateAsync(newCourse);

    // Update a specific course based on Id.
    public async Task UpdateAsync(string id, Course updatedCourse) => await _coursesRepository.UpdateAsync(id, updatedCourse);

    // Remove a specific course based on Id.
    public async Task RemoveAsync(string id) => await _coursesRepository.RemoveAsync(id);

    // Get all the students that have purchased the course.
    public async Task<List<Student>> GetParticipantsAsync(string id) => await _coursesRepository.GetParticipantsAsync(id);
}
