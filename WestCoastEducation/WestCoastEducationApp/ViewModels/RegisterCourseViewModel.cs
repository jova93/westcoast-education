using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace WestCoastEducationApp.ViewModels;

public class RegisterCourseViewModel
{
    [Required(ErrorMessage = "Number must be indicated!")]
    public string Number { get; set; } = null!;

    [Required(ErrorMessage = "Title must be indicated!")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Description must be indicated!")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "Duration must be indicated!")]
    public int Duration { get; set; }

    [Required(ErrorMessage = "Level must be indicated!")]
    public string Level { get; set; } = null!;

    [Required(ErrorMessage = "IsActive must be indicated!")]
    public bool IsActive { get; set; }

    [Required(ErrorMessage = "Price must be indicated!")]
    public decimal Price { get; set; }
}
