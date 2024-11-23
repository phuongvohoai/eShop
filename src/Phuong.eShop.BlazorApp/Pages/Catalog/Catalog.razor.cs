namespace Phuong.eShop.BlazorApp.Pages.Catalog;

public partial class Catalog : ComponentBase
{
    [CascadingParameter]
    public required PageInfo PageInfo { get; set; }

    protected override void OnInitialized()
    {
        PageInfo.PageTitle = "Catalog";
    }
}