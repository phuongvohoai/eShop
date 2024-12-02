using Microsoft.AspNetCore.Authorization;
using Phuong.eShop.CatalogService.Application.CatalogBrands.Commands;
using Phuong.eShop.CatalogService.Application.CatalogBrands.Models;
using Phuong.eShop.CatalogService.Application.CatalogBrands.Queries;
namespace Phuong.eShop.CatalogService.Controllers;

[Route("api/catalog/brands")]
[Authorize]
public class CatalogBrandsController : BaseApiController
{
    [HttpGet]
    public Task<ApiResponse<List<CatalogBrandDto>>> Get()
    {
        return Mediator.Send(new GetCatalogBrandsQuery());
    }

    [HttpGet("{id:long}")]
    public Task<ApiResponse<CatalogBrandDto>> GetById(long id)
    {
        return Mediator.Send(new GetCatalogBrandByIdQuery(id));
    }

    [HttpPost]
    public Task<ApiResponse<CatalogBrandDto>> Create([FromBody] CreateCatalogBrandCommand command)
    {
        return Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public Task<ApiResponse<CatalogBrandDto>> Update(int id, [FromBody] UpdateCatalogBrandCommand command)
    {
        return Mediator.Send(command with
        {
            Id = id
        });
    }

    [HttpDelete("{id:long}")]
    public Task<ApiResponse<bool>> DeleteById(long id)
    {
        return Mediator.Send(new DeleteCatalogBrandCommand(id));
    }
}