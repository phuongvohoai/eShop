using Phuong.eShop.CatalogService.Application.CatalogTypes.Commands;
using Phuong.eShop.CatalogService.Application.CatalogTypes.Models;
using Phuong.eShop.CatalogService.Application.CatalogTypes.Queries;

namespace Phuong.eShop.CatalogService.Controllers;

[Route("api/catalog/types")]
public class CatalogTypesController : BaseApiController
{
    [HttpGet]
    public Task<ApiResponse<List<CatalogTypeDto>>> Get()
    {
        return Mediator.Send(new GetCatalogTypesQuery());
    }
    [HttpGet("{id:long}")]
    public Task<ApiResponse<CatalogTypeDto>> GetById(long id)
    {
        return Mediator.Send(new GetCatalogTypeById(id));
    }

    [HttpPost]
    public Task<ApiResponse<CatalogTypeDto>> Create([FromBody] CreateCatalogTypeCommand request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:long}")]
    public Task<ApiResponse<CatalogTypeDto>> Update(long id, [FromBody] UpdateCatalogTypeCommand request)
    {
        return Mediator.Send(request with {Id = id,});
    }
    [HttpDelete("{id:long}")]
    public Task<ApiResponse<bool>> DeleteById(long id)
    {
        return Mediator.Send(new DeleteCatalogTypeCommand(id));
    }


}