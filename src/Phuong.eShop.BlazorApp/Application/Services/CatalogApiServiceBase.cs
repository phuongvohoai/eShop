using System.Net.Http.Json;

namespace Phuong.eShop.BlazorApp.Application.Services;

public abstract class CatalogApiServiceBase(IHttpClientFactory clientFactory)
{
    protected HttpClient HttpClient { get; } = clientFactory.CreateClient("CatalogApi");

    public async Task<List<TResponse>> GetAllAsync<TResponse>(string url)
    {
        var apiResponse = await HttpClient.GetFromJsonAsync<ApiResponse<List<TResponse>>>(url);
        if (apiResponse is null || !apiResponse.IsSuccess)
        {
            return [];
        }
        return apiResponse.Value;
    }

    public async Task<TResponse?> GetByIdAsync<TResponse>(string url)
    {
        var apiResponse = await HttpClient.GetFromJsonAsync<ApiResponse<TResponse>>(url);
        if (apiResponse is null || !apiResponse.IsSuccess)
        {
            return default;
        }
        return apiResponse.Value;
    }

    public async Task<TResponse?> PostAsync<TResponse>(string url, object entity)
    {
        var response = await HttpClient.PostAsJsonAsync(url, entity);
        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<TResponse>>();
        if (apiResponse is null || !apiResponse.IsSuccess)
        {
            return default;
        }
        return apiResponse.Value;
    }

    public async Task<TResponse?> PutAsync<TResponse>(string url, object entity)
    {
        var response = await HttpClient.PutAsJsonAsync(url, entity);
        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<TResponse>>();
        if (apiResponse is null || !apiResponse.IsSuccess)
        {
            return default;
        }
        return apiResponse.Value;
    }

    public async Task<TResponse?> PatchAsync<TResponse>(string url, object entity)
    {
        var response = await HttpClient.PatchAsJsonAsync(url, entity);
        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<TResponse>>();
        if (apiResponse is null || !apiResponse.IsSuccess)
        {
            return default;
        }
        return apiResponse.Value;
    }

    public async Task<bool> DeleteAsync(string url)
    {
        var response = await HttpClient.DeleteFromJsonAsync<ApiResponse<bool>>(url);
        return response is not null && response.IsSuccess;
    }
}


public record ApiResponse<T>(T Value, bool IsSuccess);