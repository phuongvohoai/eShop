using MudBlazor;

namespace Phuong.eShop.BlazorApp.Pages.Catalog;

public partial class Catalog : ComponentBase
{
    [Inject]
    private PageInfo PageInfo { get; set; } = default!;

    [Inject]
    private ICatalogTypeService CatalogTypeService { get; set; } = default!;

    [Inject]
    private ICatalogProductService CatalogProductService { get; set; } = default!;

    [Inject]
    private ICatalogBrandService CatalogBrandService { get; set; } = default!;

    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = default!;

    private string _searchString = string.Empty;
    private List<CatalogTypeDto> _catalogTypes = [];
    private List<CatalogBrandDto> _catalogBrands = [];
    private List<CatalogProductDto> _catalogItems = [];
    private HashSet<CatalogProductDto> _selectedCatalogItems = [];

    protected override async Task OnInitializedAsync()
    {
        PageInfo.PageTitle = "Catalog";

        var catalogTypesTask = CatalogTypeService.GetAllAsync();
        var catalogBrandsTask = CatalogBrandService.GetAllAsync();
        var catalogItemsTask = CatalogProductService.GetAllAsync();
        await Task.WhenAll(catalogTypesTask, catalogBrandsTask, catalogItemsTask);

        _catalogTypes = catalogTypesTask.Result;
        _catalogBrands = catalogBrandsTask.Result;
        _catalogItems = catalogItemsTask.Result.OrderBy(x => x.Name).ToList();

        foreach (var productDto in _catalogItems)
        {
            PopulateBrandAndType(productDto);
        }
    }

    private void PopulateBrandAndType(CatalogProductDto catalogProduct)
    {
        catalogProduct.CatalogTypeNavigation = _catalogTypes.FirstOrDefault(x => x.Id == catalogProduct.CatalogTypeId);
        catalogProduct.CatalogBrandNavigation = _catalogBrands.FirstOrDefault(x => x.Id == catalogProduct.CatalogBrandId);
    }

    private Func<CatalogProductDto, bool> QuickFilter =>
        x => string.IsNullOrWhiteSpace(_searchString) || string.IsNullOrWhiteSpace(x.Name) || x.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase);

    private async Task OnAdd()
    {
        var parameters = new DialogParameters<CreateOrUpdateCatalogDialog>
        {
            {
                x => x.CatalogProduct, new CatalogProductDto()
            },
            {
                x => x.CatalogTypes, _catalogTypes
            },
            {
                x => x.CatalogBrands, _catalogBrands
            }
        };
        var dialogReference = await DialogService.ShowAsync<CreateOrUpdateCatalogDialog>("Create Product", parameters);
        var addedCatalogItem = await dialogReference.GetReturnValueAsync<CatalogProductDto>();
        if (addedCatalogItem != null)
        {
            PopulateBrandAndType(addedCatalogItem);
            _catalogItems.Add(addedCatalogItem);
            Snackbar.Add($"Product '{addedCatalogItem.Name}' created", Severity.Success);
        }
    }

    private async Task OnEdit(CatalogProductDto catalogProduct)
    {
        var parameters = new DialogParameters<CreateOrUpdateCatalogDialog>
        {
            {
                x => x.CatalogProduct, catalogProduct
            },
            {
                x => x.CatalogTypes, _catalogTypes
            },
            {
                x => x.CatalogBrands, _catalogBrands
            }
        };
        var dialogReference = await DialogService.ShowAsync<CreateOrUpdateCatalogDialog>($"Update Product #{catalogProduct.Id}", parameters);
        var updatedCatalogItem = await dialogReference.GetReturnValueAsync<CatalogProductDto>();
        if (updatedCatalogItem != null)
        {
            PopulateBrandAndType(updatedCatalogItem);
            var index = _catalogItems.FindIndex(x => x.Id == updatedCatalogItem.Id);
            _catalogItems[index] = updatedCatalogItem;
            Snackbar.Add($"Product '{updatedCatalogItem.Name}' updated", Severity.Success);
        }
    }

    private async Task OnDelete(CatalogProductDto catalog)
    {
        var dialogResult = await DialogService.ShowMessageBox("Delete Product", $"Are you sure you want to delete the product '{catalog.Name}'?");
        if (dialogResult == true)
        {
            await CatalogTypeService.DeleteAsync(catalog.Id);
            _catalogItems.Remove(catalog);
            Snackbar.Add($"Product '{catalog.Name}' deleted", Severity.Success);
        }
    }
}