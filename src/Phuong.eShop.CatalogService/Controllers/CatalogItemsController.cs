using Phuong.eShop.CatalogService.Application.CatalogItems.Queries;

namespace Phuong.eShop.CatalogService.Controllers;

[Route("api/catalog/items")]
public class CatalogItemsController : BaseApiController
{
    [HttpGet]
    public Task<List<CatalogItemDto>> Get()
    {
        return Mediator.Send(new GetCatalogItemsQuery());
    }
}