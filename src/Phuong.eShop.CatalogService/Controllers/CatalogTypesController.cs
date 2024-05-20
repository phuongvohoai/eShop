using Phuong.eShop.CatalogService.Application.CatalogTypes.Queries;

namespace Phuong.eShop.CatalogService.Controllers;

[Route("api/catalog/types")]
public class CatalogTypesController : BaseApiController
{
    [HttpGet]
    public Task<List<CatalogTypeDto>> Get()
    {
        return Mediator.Send(new GetCatalogTypesQuery());
    }
}