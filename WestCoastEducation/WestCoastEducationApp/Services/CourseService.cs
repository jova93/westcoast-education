using System.Text;
using System.Text.Json;
using WestCoastEducationApp.Models;
using WestCoastEducationApp.Services.Interfaces;
using WestCoastEducationApp.ViewModels;

namespace WestCoastEducationApp.Services;

public class CourseService : ICourseService
{
    private readonly string _baseUrl;
    private readonly JsonSerializerOptions _options;
    private readonly HttpClient _httpClient;

    public CourseService(IConfiguration configuration, HttpClient httpClient)
    {
        _baseUrl = configuration.GetSection("api:baseUrl").Value + "/courses";

        _httpClient = httpClient;

        _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }
    public async Task<bool> CreateAsync(CourseModel model)
    {
        try
        {
            var url = _baseUrl;
            var data = JsonSerializer.Serialize(model);

            var response = await _httpClient.PostAsync(url, new StringContent(data, Encoding.Default, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<CourseModel>> GetAsync()
    {
        var response = await _httpClient.GetAsync(_baseUrl);

        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<CourseModel>>(data, _options);

            return result!;
        }

        throw new Exception("Not able to process request!");
    }

    public async Task<CourseModel> GetAsync(string id)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");

        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<CourseModel>(data, _options);

            return result!;
        }

        throw new Exception("Not able to process request!");
    }

    public async Task<CourseModel> GetByNumberAsync(string number)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/find/{number}");

        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<CourseModel>(data, _options);

            return result!;
        }

        throw new Exception("Not able to process request!");
    }

    public async Task<bool> RemoveAsync(string id)
    {
        try
        {
            var url = $"{_baseUrl}/{id}";

            var response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            var error = await response.Content.ReadAsStringAsync();

            throw new Exception(error);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> UpdateAsync(string id, UpdateCourseViewModel viewModel)
    {
        try
        {
            var url = $"{_baseUrl}/{id}";

            var data = JsonSerializer.Serialize(viewModel);

            var response = await _httpClient.PutAsync(url, new StringContent(data, Encoding.Default, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            var error = await response.Content.ReadAsStringAsync();

            throw new Exception(error);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
