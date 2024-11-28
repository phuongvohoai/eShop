using MudBlazor;

namespace Phuong.eShop.BlazorApp.Pages.Catalog;

public partial class Catalog : ComponentBase
{
    [Inject]
    private PageInfo PageInfo { get; set; } = default!;

    [Inject]
    private ICatalogTypeService CatalogTypeService { get; set; } = default!;

    [Inject]
    private ICatalogService CatalogService { get; set; } = default!;

    [Inject]
    private ICatalogBrandService CatalogBrandService { get; set; } = default!;

    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = default!;

    private string _searchString = string.Empty;
    private List<CatalogTypeDto> _catalogTypes = [];
    private List<CatalogBrandDto> _catalogBrands = [];
    private List<CatalogItemDto> _catalogItems = [];
    private HashSet<CatalogItemDto> _selectedCatalogItems = [];

    protected override async Task OnInitializedAsync()
    {
        PageInfo.PageTitle = "Catalog";

        var catalogTypesTask = CatalogTypeService.GetAllAsync();
        var catalogBrandsTask = CatalogBrandService.GetAllAsync();
        var catalogItemsTask = CatalogService.GetAllAsync();
        await Task.WhenAll(catalogTypesTask, catalogBrandsTask, catalogItemsTask);

        _catalogTypes = catalogTypesTask.Result;
        _catalogBrands = catalogBrandsTask.Result;
        _catalogItems = catalogItemsTask.Result;
    }

    private Func<CatalogItemDto, bool> QuickFilter =>
        x => string.IsNullOrWhiteSpace(_searchString) || string.IsNullOrWhiteSpace(x.Name) || x.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase);

    private async Task OnAdd()
    {
        var parameters = new DialogParameters<CreateOrUpdateCatalogDialog>
        {
            {
                x => x.CatalogItem, new CatalogItemDto() 
            },
            {
                x => x.CatalogTypes, _catalogTypes
            },
            {
                x => x.CatalogBrands, _catalogBrands
            }
        };
        var dialogReference = await DialogService.ShowAsync<CreateOrUpdateCatalogDialog>("Create Catalog", parameters);
        var addedCatalogItem = await dialogReference.GetReturnValueAsync<CatalogItemDto>();
        if (addedCatalogItem != null)
        {
            _catalogItems.Add(addedCatalogItem);
            Snackbar.Add($"Catalog '{addedCatalogItem.Name}' created", Severity.Success);
        }
    }

    private async Task OnEdit(CatalogItemDto catalogItem)
    {
        var parameters = new DialogParameters<CreateOrUpdateCatalogDialog>
        {
            {
                x => x.CatalogItem, catalogItem
            },
            {
                x => x.CatalogTypes, _catalogTypes
            },
            {
                x => x.CatalogBrands, _catalogBrands
            }
        };
        var dialogReference = await DialogService.ShowAsync<CreateOrUpdateCatalogDialog>($"Update Catalog #{catalogItem.Id}", parameters);
        var updatedCatalogItem = await dialogReference.GetReturnValueAsync<CatalogItemDto>();
        if (updatedCatalogItem != null)
        {
            var index = _catalogTypes.FindIndex(x => x.Id == updatedCatalogItem.Id);
            _catalogItems[index] = updatedCatalogItem;
            Snackbar.Add($"Catalog '{updatedCatalogItem.Name}' updated", Severity.Success);
        }
    }

    private async Task OnDelete(CatalogItemDto catalog)
    {
        var dialogResult = await DialogService.ShowMessageBox("Delete Catalog", $"Are you sure you want to delete the catalog '{catalog.Name}'?");
        if (dialogResult == true)
        {
            await CatalogTypeService.DeleteAsync(catalog.Id);
            _catalogItems.Remove(catalog);
            Snackbar.Add($"Catalog '{catalog.Name}' deleted", Severity.Success);
        }
    }
}