using WestCoastEducationApi.Models;
using WestCoastEducationApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WestCoastEducationApi.ViewModels;

namespace WestCoastEducationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentsService _studentsService;

    public StudentsController(IStudentsService studentsService)
    {
        _studentsService = studentsService;
    }

    //[HttpGet]
    //public async Task<List<Student>> Get() => await _studentsService.GetAsync();

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _studentsService.GetAsync();

        if (result is null)
        {
            return NotFound();
        }

        var students = new List<StudentViewModel>();

        foreach (var student in result)
        {
            var temp = new StudentViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                Address = student.Address,
                TotalSpent = student.TotalSpent,
                PurchasedCourses = student.PurchasedCourses
            };

            students.Add(temp);
        }

        return Ok(students);
    }

    //[HttpGet("{id:length(24)}")]
    //public async Task<ActionResult<Student>> Get(string id)
    //{
    //    var student = await _studentsService.GetAsync(id);

    //    if (student is null)
    //    {
    //        return NotFound();
    //    }

    //    return student;
    //}

    [HttpGet("{id:length(24)}")]
    public async Task<IActionResult> Get(string id)
    {
        try
        {
            var student = await _studentsService.GetAsync(id);

            if (student is null)
            {
                return NotFound();
            }

            var viewModel = new StudentViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                Address = student.Address,
                TotalSpent = student.TotalSpent,
                PurchasedCourses = student.PurchasedCourses
            };

            return Ok(viewModel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("find/{email}")]
    public async Task<IActionResult> Get(string email, bool unused = false)
    {
        try
        {
            var student = await _studentsService.GetByEmailAsync(email);

            if (student is null)
            {
                return NotFound();
            }

            var viewModel = new StudentViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                Address = student.Address,
                TotalSpent = student.TotalSpent,
                PurchasedCourses = student.PurchasedCourses
            };

            return Ok(viewModel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    //[HttpPost]
    //public async Task<IActionResult> Post(Student newStudent)
    //{
    //    await _studentsService.CreateAsync(newStudent);

    //    return CreatedAtAction(nameof(Get), new { id = newStudent.Id }, newStudent);
    //}

    [HttpPost]
    public async Task<IActionResult> Post(AddStudentViewModel viewModel)
    {
        try
        {
            var student = new Student
            {
                Id = viewModel.Id,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber,
                Address = viewModel.Address,
                TotalSpent = viewModel.TotalSpent,
                PurchasedCourses = viewModel.PurchasedCourses
            };

            await _studentsService.CreateAsync(student);

            return StatusCode(201);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    //[HttpPut("{id:length(24)}")]
    //public async Task<IActionResult> Update(string id, Student updatedStudent)
    //{
    //    var student = await _studentsService.GetAsync(id);

    //    if (student is null)
    //    {
    //        return NotFound();
    //    }

    //    updatedStudent.Id = student.Id;

    //    await _studentsService.UpdateAsync(id, updatedStudent);

    //    return NoContent();
    //}

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Put(string id, UpdateStudentViewModel viewModel)
    {
        var student = await _studentsService.GetAsync(id);

        if (student is null)
        {
            return NotFound();
        }

        student.FirstName = viewModel.FirstName;
        student.LastName = viewModel.LastName;
        student.Email = viewModel.Email;
        student.PhoneNumber = viewModel.PhoneNumber;
        student.Address = viewModel.Address;
        student.TotalSpent = viewModel.TotalSpent;
        student.PurchasedCourses = viewModel.PurchasedCourses;

        await _studentsService.UpdateAsync(id, student);

        return NoContent();
    }

    //[HttpDelete("{id:length(24)}")]
    //public async Task<IActionResult> Delete(string id)
    //{
    //    var student = await _studentsService.GetAsync(id);

    //    if (student is null)
    //    {
    //        return NotFound();
    //    }

    //    await _studentsService.RemoveAsync(id);

    //    return NoContent();
    //}

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var student = await _studentsService.GetAsync(id);

            if (student is null)
            {
                return NotFound();
            }

            await _studentsService.RemoveAsync(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
