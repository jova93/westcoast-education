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

    //[HttpGet]
    //public async Task<List<Course>> Get() => await _coursesService.GetAsync();

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _coursesService.GetAsync();

        if (result is null)
        {
            return NotFound();
        }

        var courses = new List<CourseViewModel>();

        foreach (var course in result)
        {
            var temp = new CourseViewModel
            {
                Id = course.Id,
                Number = course.Number,
                Title = course.Title,
                Description = course.Description,
                Duration = course.Duration,
                Level = course.Level,
                IsActive = course.IsActive,
                Price = course.Price,
                Participants = course.Participants
            };

            courses.Add(temp);
        }

        return Ok(courses);
    }

    //[HttpGet("{id:length(24)}")]
    //public async Task<ActionResult<Course>> Get(string id)
    //{
    //    var course = await _coursesService.GetAsync(id);

    //    if (course is null)
    //    {
    //        return NotFound();
    //    }

    //    return course;
    //}

    [HttpGet("{id:length(24)}")]
    public async Task<IActionResult> Get(string id)
    {
        try
        {
            var course = await _coursesService.GetAsync(id);

            if (course is null)
            {
                return NotFound();
            }

            var viewModel = new CourseViewModel
            {
                Id = course.Id,
                Number = course.Number,
                Title = course.Title,
                Description = course.Description,
                Duration = course.Duration,
                Level = course.Level,
                IsActive = course.IsActive,
                Price = course.Price,
                Participants = course.Participants
            };

            return Ok(viewModel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }


    //[HttpPost]
    //public async Task<IActionResult> Post(Course newCourse)
    //{
    //    await _coursesService.CreateAsync(newCourse);

    //    return CreatedAtAction(nameof(Get), new { id = newCourse.Id }, newCourse);
    //}

    [HttpPost]
    public async Task<IActionResult> Post(AddCourseViewModel viewModel)
    {
        try
        {
            var course = new Course
            {
                Id = viewModel.Id,
                Number = viewModel.Number,
                Title = viewModel.Title,
                Description = viewModel.Description,
                Duration = viewModel.Duration,
                Level = viewModel.Level,
                IsActive = viewModel.IsActive,
                Price = viewModel.Price,
                Participants = viewModel.Participants
            };

            await _coursesService.CreateAsync(course);

            return StatusCode(201);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    //[HttpPut("{id:length(24)}")]
    //public async Task<IActionResult> Update(string id, Course updatedCourse)
    //{
    //    var course = await _coursesService.GetAsync(id);

    //    if (course is null)
    //    {
    //        return NotFound();
    //    }

    //    updatedCourse.Id = course.Id;

    //    await _coursesService.UpdateAsync(id, updatedCourse);

    //    return NoContent();
    //}

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, UpdateCourseViewModel viewModel)
    {
        var course = await _coursesService.GetAsync(id);

        if (course is null)
        {
            return NotFound();
        }

        course.Number = viewModel.Number;
        course.Title = viewModel.Title;
        course.Description = viewModel.Description;
        course.Duration = viewModel.Duration;
        course.Level = viewModel.Level;
        course.IsActive = viewModel.IsActive;
        course.Price = viewModel.Price;
        course.Participants = viewModel.Participants;

        await _coursesService.UpdateAsync(id, course);

        return NoContent();
    }

    //[HttpDelete("{id:length(24)}")]
    //public async Task<IActionResult> Delete(string id)
    //{
    //    var course = await _coursesService.GetAsync(id);

    //    if (course is null)
    //    {
    //        return NotFound();
    //    }

    //    await _coursesService.RemoveAsync(id);

    //    return NoContent();
    //}

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var course = await _coursesService.GetAsync(id);

            if (course is null)
            {
                return NotFound();
            }

            await _coursesService.RemoveAsync(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
