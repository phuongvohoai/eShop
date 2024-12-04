using Phuong.eShop.CatalogService.Application.CatalogProducts.Commands;
using Phuong.eShop.CatalogService.Application.CatalogProducts.Models;
using Phuong.eShop.CatalogService.Application.CatalogProducts.Queries;
using Phuong.eShop.CatalogService.Application.Common;

namespace Phuong.eShop.CatalogService.Controllers
{
    [Route("api/catalog/products")]
    public class CatalogProductsController : BaseApiController
    {
        [HttpGet]
        public Task<ApiResponse<PaginatedList<CatalogProductDto>>> GetWithPagination(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int type = 0,
            [FromQuery] int brand = 0,
            [FromQuery] string searchString = "")
        {
            return Mediator.Send(new GetCatalogProductWithPaginationQuery
            {
                PageNumber = pageNumber,
                Brand = brand,
                Type = type,
                PageSize = pageSize,
                SearchString = searchString
            });
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

        [HttpDelete("bulk")]
        public Task<ApiResponse<bool>> BulkDelete([FromBody] List<long> ids)
        {
            return Mediator.Send(new BulkDeleteCatalogProductCommand(ids));
        }

        [HttpPut("bulk")]
        public Task<ApiResponse<List<CatalogProductDto>>> BulkUpdate([FromBody] List<CatalogProductDto> command)
        {
            return Mediator.Send(new BulkUpdateCatalogProductCommand(command));
        }
    }
}

