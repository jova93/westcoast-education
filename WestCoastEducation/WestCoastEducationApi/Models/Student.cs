using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WestCoastEducationApi.Models;

public class Student
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Address { get; set; } = null!;

    // This keeps track on how much the student has spent on our company.
    public decimal TotalSpent { get; set; }

    // This is the courses that a student has bought.
    public ICollection<ObjectId>? PurchasedCourses { get; set; }
}


