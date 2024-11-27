using System.Net.Http.Json;

namespace Phuong.eShop.BlazorApp.Application.Services;

public abstract class CatalogApiServiceBase(IHttpClientFactory clientFactory)
{
    protected HttpClient HttpClient { get; } = clientFactory.CreateClient("CatalogApi");

    public async Task<List<T>> GetAllAsync<T>(string url)
    {
        var apiResponse = await HttpClient.GetFromJsonAsync<ApiResponse<List<T>>>(url);
        if (apiResponse is null || !apiResponse.Success)
        {
            return [];
        }
        return apiResponse.Value;
    }

    public async Task<T?> GetByIdAsync<T>(string url)
    {
        var apiResponse = await HttpClient.GetFromJsonAsync<ApiResponse<T>>(url);
        if (apiResponse is null || !apiResponse.Success)
        {
            return default;
        }
        return apiResponse.Value;
    }

    public async Task<T?> PostAsync<T>(string url, T entity)
    {
        var response = await HttpClient.PostAsJsonAsync(url, entity);
        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<T>>();
        if (apiResponse is null || !apiResponse.Success)
        {
            return default;
        }
        return apiResponse.Value;
    }

    public async Task<T?> PutAsync<T>(string url, T entity)
    {
        var response = await HttpClient.PutAsJsonAsync(url, entity);
        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<T>>();
        if (apiResponse is null || !apiResponse.Success)
        {
            return default;
        }
        return apiResponse.Value;
    }

    public async Task<T?> PatchAsync<T>(string url, T entity)
    {
        var response = await HttpClient.PatchAsJsonAsync(url, entity);
        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<T>>();
        if (apiResponse is null || !apiResponse.Success)
        {
            return default;
        }
        return apiResponse.Value;
    }

    public async Task<bool> DeleteAsync(string url)
    {
        var response = await HttpClient.DeleteFromJsonAsync<ApiResponse<bool>>(url);
        return response is not null && response.Success;
    }
}


public record ApiResponse<T>(T Value, bool Success);