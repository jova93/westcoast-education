using WestCoastEducationApi.Models;
using WestCoastEducationApi.Services;
using WestCoastEducationApi.Services.Interfaces;
using WestCoastEducationApi.Repositories;
using WestCoastEducationApi.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<WestCoastEducationStoreDatabaseSettings>(builder.Configuration.GetSection("WestCoastEducationStoreDatabase"));

builder.Services.AddScoped<ICoursesRepository, CoursesRepository>();
builder.Services.AddScoped<IStudentsRepository, StudentsRepository>();
builder.Services.AddScoped<ICoursesService, CoursesService>();
builder.Services.AddScoped<IStudentsService, StudentsService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
