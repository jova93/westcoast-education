using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using WestCoastEducationApp.Models;
using WestCoastEducationApp.Services.Interfaces;
using WestCoastEducationApp.ViewModels;

namespace WestCoastEducationApp.Controllers;

public class CoursesController : Controller
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string searchString)
    {
        var result = await _courseService.GetAsync();

        if (!string.IsNullOrEmpty(searchString))
        {
            var resultFiltered = result.Where(c => c.Number == searchString);

            return View("Index", resultFiltered);
        }

        return View("Index", result);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(RegisterCourseViewModel inData)
    {
        if(!ModelState.IsValid)
        {
            return View("Create", inData);
        }

        var course = new CourseModel
        {
            Number = inData.Number,
            Title = inData.Title,
            Description = inData.Description,
            Duration = inData.Duration,
            Level = inData.Level,
            IsActive = inData.IsActive,
            Price = inData.Price,
            Participants = new List<ObjectId>()
        };

        try
        {
            if (await _courseService.CreateAsync(course))
            {
                return RedirectToAction("Index");
            }
        }
        catch (Exception)
        {
            return View("Error");
        }

        return View("Error");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var course = await _courseService.GetAsync(id);

        var model = new EditCourseViewModel
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

        return View("Edit", model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditCourseViewModel inData)
    {
        try
        {
            var course = await _courseService.GetAsync(inData.Id!);

            var model = new UpdateCourseViewModel
            {
                Number = inData.Number,
                Title = inData.Title,
                Description = inData.Description,
                Duration = inData.Duration,
                Level = inData.Level,
                IsActive = inData.IsActive,
                Price = inData.Price,
                Participants = inData.Participants
            };

            if (await _courseService.UpdateAsync(inData.Id!, model))
            {
                return RedirectToAction("Index");
            }

            return View("Error");
        }
        catch (Exception)
        {
            return View("Error");
        }
    }

    public async Task<IActionResult> Details(string number)
    {
        var result = await _courseService.GetByNumberAsync(number);

        if (result is null)
        {
            return Content($"Couldn't find course with number {number}");
        }

        return Content($"Course with number {number}");
    }

    public async Task<IActionResult> Delete(string number)
    {
        var course = await _courseService.GetByNumberAsync(number);

        var id = course.Id!;
        
        if (await _courseService.RemoveAsync(id))
        {
            return RedirectToAction("Index");
        }

        return View("Error");
    }
}
