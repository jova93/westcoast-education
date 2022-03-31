using WestCoastEducationApp.Models;
using WestCoastEducationApp.ViewModels;

namespace WestCoastEducationApp.Services.Interfaces;

public interface ICourseService
{
    Task<List<CourseModel>> GetAsync();
    Task<CourseModel> GetAsync(string id);
    Task<CourseModel> GetByNumberAsync(string number);
    Task<bool> CreateAsync(CourseModel model);
    Task<bool> UpdateAsync(string id, UpdateCourseViewModel viewModel);
    Task<bool> RemoveAsync(string id);
}
