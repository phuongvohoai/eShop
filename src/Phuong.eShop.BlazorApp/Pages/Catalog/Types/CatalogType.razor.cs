using MudBlazor;

namespace Phuong.eShop.BlazorApp.Pages.Catalog.Types;

public partial class CatalogType : ComponentBase
{
    [Inject]
    public required PageInfo PageInfo { get; set; }

    [Inject]
    public required ICatalogTypeService CatalogTypeService { get; set; }

    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = default!;

    private string _searchString = string.Empty;
    private List<CatalogTypeDto> _catalogTypes = [];
    private HashSet<CatalogTypeDto> _selectedCatalogTypes = [];

    protected override async Task OnInitializedAsync()
    {
        PageInfo.PageTitle = "Catalog Type";
        _catalogTypes = await CatalogTypeService.GetAllAsync();
    }

    private Func<CatalogTypeDto, bool> QuickFilter =>
        x => string.IsNullOrWhiteSpace(_searchString) || x.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase);

    private async Task OnAdd()
    {
        var parameters = new DialogParameters<CreateOrUpdateCatalogTypeDialog> { { x => x.CatalogType, new CatalogTypeDto() } };
        var dialogReference = await DialogService.ShowAsync<CreateOrUpdateCatalogTypeDialog>("Create Catalog Type", parameters);
        var addedType = await dialogReference.GetReturnValueAsync<CatalogTypeDto>();
        if (addedType != null)
        {
            _catalogTypes.Add(addedType);
            Snackbar.Add($"Catalog type '{addedType.Name}' created", Severity.Success);
        }
    }

    private async Task OnEdit(CatalogTypeDto catalogType)
    {
        var parameters = new DialogParameters<CreateOrUpdateCatalogTypeDialog> { { x => x.CatalogType, catalogType } };
        var dialogReference = await DialogService.ShowAsync<CreateOrUpdateCatalogTypeDialog>($"Update Catalog Type #{catalogType.Id}", parameters);
        var updatedType = await dialogReference.GetReturnValueAsync<CatalogTypeDto>();
        if (updatedType != null)
        {
            var index = _catalogTypes.FindIndex(x => x.Id == updatedType.Id);
            _catalogTypes[index] = updatedType;
            Snackbar.Add($"Catalog type '{updatedType.Name}' updated", Severity.Success);
        }
    }

    private async Task OnDelete(CatalogTypeDto catalogType)
    {
        var dialogResult = await DialogService.ShowMessageBox("Delete Catalog Type", $"Are you sure you want to delete the catalog type '{catalogType.Name}'?");
        if (dialogResult == true)
        {
            await CatalogTypeService.DeleteAsync(catalogType.Id);
            _catalogTypes.Remove(catalogType);
            Snackbar.Add($"Catalog type '{catalogType.Name}' deleted", Severity.Success);
        }
    }
}