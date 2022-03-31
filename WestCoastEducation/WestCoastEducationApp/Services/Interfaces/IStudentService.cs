using WestCoastEducationApp.Models;
using WestCoastEducationApp.ViewModels;

namespace WestCoastEducationApp.Services.Interfaces;

public interface IStudentService
{
    Task<List<StudentModel>> GetAsync();
    Task<StudentModel> GetAsync(string id);
    Task<StudentModel> GetByEmailAsync(string email);
    Task<bool> CreateAsync(StudentModel model);
    Task<bool> UpdateAsync(string id, UpdateStudentViewModel viewModel);
    Task<bool> RemoveAsync(string id);
}
