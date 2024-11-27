using Phuong.eShop.CatalogService.Application.CatalogBrands.Commands;
using Phuong.eShop.CatalogService.Application.CatalogBrands.Models;
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

    [HttpPost]
    public Task<ApiResponse<CatalogBranchDto>> Create([FromBody] CreateCatalogBranchCommand command)
    {
        return Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public Task<ApiResponse<CatalogBranchDto>> Update(int id, [FromBody] UpdateCatalogBranchCommand command)
    {
        return Mediator.Send(command with
        {
            Id = id
        });
    }
}