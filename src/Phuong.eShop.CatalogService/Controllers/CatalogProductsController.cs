using Phuong.eShop.CatalogService.Application.CatalogItems.Queries;
using Phuong.eShop.CatalogService.Application.CatalogProducts.Commands;
using Phuong.eShop.CatalogService.Application.CatalogProducts.Models;
using Phuong.eShop.CatalogService.Application.CatalogProducts.Queries;

namespace Phuong.eShop.CatalogService.Controllers
{
    [Route("api/catalog/products")]
    public class CatalogProductsController : BaseApiController
    {
        [HttpGet]
        public Task<ApiResponse<List<CatalogProductDto>>> Get()
        {
            return Mediator.Send(new GetCatalogProductQuery());
        }

        [HttpGet("{id:long}")]
        public Task<ApiResponse<CatalogProductDto>> GetById(long id)
        {
            return Mediator.Send(new GetCatalogProductByIdQuery(id));
        }

        [HttpPost]
        public Task<ApiResponse<CatalogProductDto>> Create([FromBody] CatalogProductDto catalogProduct)
        {
            var createCommand = new CreateCatalogProductCommand(catalogProduct);
            return Mediator.Send(createCommand);
        }

        [HttpPut("{id:long}")]
        public Task<ApiResponse<CatalogProductDto>> UpdateById(long id, [FromBody] CatalogProductDto catalogProduct)
        {
            catalogProduct.Id = id;
            var updateCommand = new UpdateCatalogProductCommand(catalogProduct);
            return Mediator.Send(updateCommand);
        }

        [HttpDelete("{id:long}")]
        public Task<ApiResponse<bool>> DeleteById(long id)
        {
            return Mediator.Send(new DeleteCatalogProductCommand(id));
        }
    }
}

