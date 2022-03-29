using WestCoastEducationApi.Models;
using WestCoastEducationApi.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace WestCoastEducationApi.Repositories;

public class CoursesRepository : ICoursesRepository
{
    // This is the Courses collection in the database.
    private readonly IMongoCollection<Course> _coursesCollection;

    // This is the Students collection in the database.
    private readonly IMongoCollection<Student> _studentsCollection;

    public CoursesRepository(IOptions<WestCoastEducationStoreDatabaseSettings> westCoastEducationStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(westCoastEducationStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(westCoastEducationStoreDatabaseSettings.Value.DatabaseName);

        _coursesCollection = mongoDatabase.GetCollection<Course>(westCoastEducationStoreDatabaseSettings.Value.CoursesCollectionName);

        _studentsCollection = mongoDatabase.GetCollection<Student>(westCoastEducationStoreDatabaseSettings.Value.StudentsCollectionName);
    }

    // Get all courses.
    public async Task<List<Course>> GetAsync() => await _coursesCollection.Find(_ => true).ToListAsync();

    // Get a specific course based on Id.
    public async Task<Course?> GetAsync(string id) => await _coursesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    // Get a specific course based on Number.
    public async Task<Course?> GetByNumberAsync(string number) => await _coursesCollection.Find(x => x.Number == number).FirstOrDefaultAsync();

    // Create a new course and add it to the Courses collection.
    public async Task CreateAsync(Course newCourse) => await _coursesCollection.InsertOneAsync(newCourse);

    // Update a specific course based on Id.
    public async Task UpdateAsync(string id, Course updatedCourse) => await _coursesCollection.ReplaceOneAsync(x => x.Id == id, updatedCourse);

    // Remove a specific course based on Id.
    public async Task RemoveAsync(string id) => await _coursesCollection.DeleteOneAsync(x => x.Id == id);

    // Get all the students that have purchased the course.
    public async Task<List<Student>> GetParticipantsAsync(string id)
    {
        // The list of participants that we will return.
        var participants = new List<Student>();

        // All students that are stored in Students collection.
        var allStudents = await _studentsCollection.Find(_ => true).ToListAsync();

        // Get the course that we want to get the participants from.
        var course = await _coursesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        // Get the collection of ObjectIds that this course has.
        var listOfObjectIds = course.Participants;

        if (listOfObjectIds != null)
        {
            // Loop through the collection above and add them to the list of participants.
            foreach (var objectId in listOfObjectIds)
            {
                // Run a query against the database to see if there is a student with a matching ObjectId.
                var tempStudent = await _studentsCollection.Find(x => x.Id == objectId.ToString()).FirstOrDefaultAsync();

                // DO NOT ADD NULL ITEMS!!
                if (tempStudent != null)
                    participants.Add(tempStudent);
            }
        }

        return participants;
    }
}
