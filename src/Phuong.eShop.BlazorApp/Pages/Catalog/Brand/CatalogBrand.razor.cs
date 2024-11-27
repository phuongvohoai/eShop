using MudBlazor;

namespace Phuong.eShop.BlazorApp.Pages.Catalog.Brand;

public partial class CatalogBrand : ComponentBase
{
    [Inject]
    private PageInfo _pageInfo { get; set; }

    [Inject]
    private ICatalogBrandService _catalogBrandService { get; set; }

    [Inject]
    private IDialogService _dialogService { get; set; }

    private string? _searchString;
    private List<CatalogBrandDto> _catalogBrands = [];
    private CatalogBrandDto _selectedCatalogBrand = new();
    private HashSet<CatalogBrandDto> _selectedCatalogBrands = [];

    protected override async Task OnInitializedAsync()
    {
        _pageInfo.PageTitle = "Catalog Brand";
        _catalogBrands = await _catalogBrandService.ListAllAsync();
    }

    private Func<CatalogBrandDto, bool> QuickFilter =>
        x => string.IsNullOrWhiteSpace(_searchString) || x.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase);

    private async Task OnAdd()
    {
        var parameters = new DialogParameters<CreateOrUpdateBrandDialog> { { x => x.CatalogBrand, new CatalogBrandDto() } };
        await _dialogService.ShowAsync<CreateOrUpdateBrandDialog>("Create Catalog Brand", parameters);
    }
    
    private async Task OnEdit(CatalogBrandDto catalogBrand)
    {
        var parameters = new DialogParameters<CreateOrUpdateBrandDialog> { { x => x.CatalogBrand, catalogBrand } };
        await _dialogService.ShowAsync<CreateOrUpdateBrandDialog>($"Update Catalog Brand #{catalogBrand.Id}", parameters);
    }
    
    private async Task OnDelete(CatalogBrandDto catalogBrand)
    {
        var dialogResult = await _dialogService.ShowMessageBox("Delete Catalog Brand", $"Are you sure you want to delete the catalog brand '{catalogBrand.Name}'?" );
        if (dialogResult == true)
        {
            await _catalogBrandService.DeleteAsync(catalogBrand.Id);
            _catalogBrands.Remove(catalogBrand);
        }
    }
}