namespace Phuong.eShop.BlazorApp.Pages.Catalog.Brand;

public partial class CatalogBrand : ComponentBase
{
    [Inject]
    public required PageInfo PageInfo { get; set; }

    [Inject]
    public required ICatalogBrandService CatalogBrandService { get; set; }

    private string? _searchString;
    private List<CatalogBrandDto> _catalogBrands = [];
    private CatalogBrandDto _selectedCatalogBrand = new();
    private HashSet<CatalogBrandDto> _selectedCatalogBrands = [];

    protected override async Task OnInitializedAsync()
    {
        PageInfo.PageTitle = "Catalog Brand";
        _catalogBrands = await CatalogBrandService.ListAllAsync();
    }

    private Func<CatalogBrandDto, bool> QuickFilter =>
        x => string.IsNullOrWhiteSpace(_searchString) || x.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase);
}