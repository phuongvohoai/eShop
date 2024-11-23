namespace Phuong.eShop.BlazorApp.Application.Models;

public class PageInfo
{
    private string? _pageTitle;

    public string? PageTitle
    {
        get => _pageTitle;
        set
        {
            _pageTitle = value;
            OnPageTitleChanged?.Invoke();
        }
    }

    public Action? OnPageTitleChanged { get; set; }
}