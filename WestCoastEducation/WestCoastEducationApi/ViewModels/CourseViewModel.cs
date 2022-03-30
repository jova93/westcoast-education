using MongoDB.Bson;

namespace WestCoastEducationApi.ViewModels;

// Are view models necessary in the REST API?
public class CourseViewModel
{
    public string? Id { get; set; }
    public string Number { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Duration { get; set; }
    public string Level { get; set; } = null!;
    public bool IsActive { get; set; }
    public decimal Price { get; set; }

    // This is the list of students that have bought this course.
    public ICollection<ObjectId>? Participants { get; set; }
}
