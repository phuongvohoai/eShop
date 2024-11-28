using MudBlazor;

namespace Phuong.eShop.BlazorApp.Pages.Catalog.Brand;

public partial class CatalogBrand : ComponentBase
{
    [Inject]
    private PageInfo PageInfo { get; set; } = default!;

    [Inject]
    private ICatalogBrandService CatalogBrandService { get; set; } = default!;

    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = default!;

    private string _searchString = string.Empty;
    private List<CatalogBrandDto> _catalogBrands = [];
    private HashSet<CatalogBrandDto> _selectedCatalogBrands = [];

    protected override async Task OnInitializedAsync()
    {
        PageInfo.PageTitle = "Catalog Brand";
        _catalogBrands = await CatalogBrandService.GetAllAsync();
    }

    private Func<CatalogBrandDto, bool> QuickFilter =>
        x => string.IsNullOrWhiteSpace(_searchString) || x.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase);

    private async Task OnAdd()
    {
        var parameters = new DialogParameters<CreateOrUpdateBrandDialog> { { x => x.CatalogBrand, new CatalogBrandDto() } };
        var dialogReference = await DialogService.ShowAsync<CreateOrUpdateBrandDialog>("Create Catalog Brand", parameters);
        var addedBrand = await dialogReference.GetReturnValueAsync<CatalogBrandDto>();
        if (addedBrand != null)
        {
            _catalogBrands.Add(addedBrand);
            Snackbar.Add($"Catalog brand '{addedBrand.Name}' created", Severity.Success);
        }
    }

    private async Task OnEdit(CatalogBrandDto catalogBrand)
    {
        var parameters = new DialogParameters<CreateOrUpdateBrandDialog> { { x => x.CatalogBrand, catalogBrand } };
        var dialogReference = await DialogService.ShowAsync<CreateOrUpdateBrandDialog>($"Update Catalog Brand #{catalogBrand.Id}", parameters);
        var updatedBrand = await dialogReference.GetReturnValueAsync<CatalogBrandDto>();
        if (updatedBrand != null)
        {
            var index = _catalogBrands.FindIndex(x => x.Id == updatedBrand.Id);
            _catalogBrands[index] = updatedBrand;
            Snackbar.Add($"Catalog brand '{updatedBrand.Name}' updated", Severity.Success);
        }
    }

    private async Task OnDelete(CatalogBrandDto catalogBrand)
    {
        var dialogResult = await DialogService.ShowMessageBox("Delete Catalog Brand", $"Are you sure you want to delete the catalog brand '{catalogBrand.Name}'?");
        if (dialogResult == true)
        {
            await CatalogBrandService.DeleteAsync(catalogBrand.Id);
            _catalogBrands.Remove(catalogBrand);
            Snackbar.Add($"Catalog brand '{catalogBrand.Name}' deleted", Severity.Success);
        }
    }
}