using WestCoastEducationApi.Models;
using WestCoastEducationApi.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace WestCoastEducationApi.Repositories;

public class StudentsRepository : IStudentsRepository
{
    // This is the Students collection in the database.
    private readonly IMongoCollection<Student> _studentsCollection;

    // This is the Courses collection in the database.
    private readonly IMongoCollection<Course> _coursesCollection;

    public StudentsRepository(IOptions<WestCoastEducationStoreDatabaseSettings> westCoastEducationStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(westCoastEducationStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(westCoastEducationStoreDatabaseSettings.Value.DatabaseName);

        _studentsCollection = mongoDatabase.GetCollection<Student>(westCoastEducationStoreDatabaseSettings.Value.StudentsCollectionName);

        _coursesCollection = mongoDatabase.GetCollection<Course>(westCoastEducationStoreDatabaseSettings.Value.CoursesCollectionName);
    }

    // Get all students.
    public async Task<List<Student>> GetAsync() => await _studentsCollection.Find(_ => true).ToListAsync();

    // Get a specific student based on Id.
    public async Task<Student?> GetAsync(string id) => await _studentsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    // Get a specific student based on Email.
    public async Task<Student?> GetByEmailAsync(string email) => await _studentsCollection.Find(x => x.Email == email).FirstOrDefaultAsync();

    // Create a new student and add it to the Students collection.
    public async Task CreateAsync(Student newStudent) => await _studentsCollection.InsertOneAsync(newStudent);

    // Update a specific student based on Id.
    public async Task UpdateAsync(string id, Student updatedStudent) => await _studentsCollection.ReplaceOneAsync(x => x.Id == id, updatedStudent);

    // Remove a specific student based on Id.
    public async Task RemoveAsync(string id) => await _studentsCollection.DeleteOneAsync(x => x.Id == id);

    // Get all the courses that the student has purchased.
    public async Task<List<Course>> GetPurchasedCoursesAsync(string id)
    {
        // The list of purchased courses that we will return.
        var purchasedCourses = new List<Course>();

        // All courses that are stored in Courses collection.
        var allCourses = await _coursesCollection.Find(_ => true).ToListAsync();

        // Get the student that we want to get the purchased courses from.
        var student = await _studentsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        // Get the collection of ObjectIds that this student has.
        var listOfObjectIds = student.PurchasedCourses;

        if (listOfObjectIds != null)
        {
            // Loop through the collection above and add them to the list of purchased.
            foreach (var objectId in listOfObjectIds)
            {
                // Run a query against the database to see if there is a course with a matching ObjectId.
                var tempCourse = await _coursesCollection.Find(x => x.Id == objectId.ToString()).FirstOrDefaultAsync();

                // DO NOT ADD NULL ITEMS!!
                if (tempCourse != null)
                    purchasedCourses.Add(tempCourse);
            }
        }

        return purchasedCourses;
    }
}
