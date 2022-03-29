namespace WestCoastEducationApi.Models;

public class WestCoastEducationStoreDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string CoursesCollectionName { get; set; } = null!;

    public string StudentsCollectionName { get; set; } = null!;
}
