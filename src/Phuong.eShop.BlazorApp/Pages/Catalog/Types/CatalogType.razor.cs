namespace Phuong.eShop.BlazorApp.Pages.Catalog.Types;

public partial class CatalogType : ComponentBase
{
    [Inject]
    public required PageInfo PageInfo { get; set; }

    [Inject]
    public required ICatalogTypeService CatalogTypeService { get; set; }

    private string? _searchString;
    private List<CatalogTypeDto> _catalogTypes = [];
    private CatalogTypeDto _selectedCatalogType = new();
    private HashSet<CatalogTypeDto> _selectedCatalogTypes = [];

    protected override async Task OnInitializedAsync()
    {
        PageInfo.PageTitle = "Catalog Type";
        _catalogTypes = await CatalogTypeService.GetAllAsync();
    }

    private Func<CatalogTypeDto, bool> QuickFilter =>
        x => string.IsNullOrWhiteSpace(_searchString) || x.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase);
}