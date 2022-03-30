using WestCoastEducationApi.Models;
using WestCoastEducationApi.Services.Interfaces;
using WestCoastEducationApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WestCoastEducationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICoursesService _coursesService;

    public CoursesController(ICoursesService coursesService)
    {
        _coursesService = coursesService;
    }

    [HttpGet]
    public async Task<List<Course>> Get() => await _coursesService.GetAsync();

    //USE THIS LATER!!
    //public async Task<IActionResult> Get()
    //{
    //    var result = await _coursesService.GetAsync();

    //    var courses = new List<CourseViewModel>();

    //    if (result is null)
    //    {
    //        return NotFound();
    //    }

    //    foreach (var course in result)
    //    {
    //        var temp = new CourseViewModel
    //        {
    //            Id = course.Id,
    //            Number = course.Number,
    //            Title = course.Title,
    //            Description = course.Description,
    //            Duration = course.Duration,
    //            Level = course.Level,
    //            IsActive = course.IsActive,
    //            Price = course.Price,
    //            Participants = course.Participants
    //        };

    //        courses.Add(temp);
    //    }

    //    return Ok(courses);
    //}

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Course>> Get(string id)
    {
        var course = await _coursesService.GetAsync(id);

        if (course is null)
        {
            return NotFound();
        }

        return course;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Course newCourse)
    {
        await _coursesService.CreateAsync(newCourse);

        return CreatedAtAction(nameof(Get), new { id = newCourse.Id }, newCourse);
    }

    //create public async Task<IActionResult> AddCourse(AddCourseViewModel model)

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Course updatedCourse)
    {
        var course = await _coursesService.GetAsync(id);

        if (course is null)
        {
            return NotFound();
        }

        updatedCourse.Id = course.Id;

        await _coursesService.UpdateAsync(id, updatedCourse);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var course = await _coursesService.GetAsync(id);

        if (course is null)
        {
            return NotFound();
        }

        await _coursesService.RemoveAsync(id);

        return NoContent();
    }
}
