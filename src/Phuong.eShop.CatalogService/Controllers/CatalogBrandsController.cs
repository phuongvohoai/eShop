using Phuong.eShop.CatalogService.Application.CatalogBrands.Queries;

namespace Phuong.eShop.CatalogService.Controllers;

[Route("api/catalog/brands")]
public class CatalogBrandsController : BaseApiController
{
    [HttpGet]
    public Task<List<CatalogBranchDto>> Get()
    {
        return Mediator.Send(new GetCatalogBrandsQuery());
    }
}