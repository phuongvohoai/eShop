using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Phuong.eShop.CatalogService.Application.CatalogItems.Commands;
using Phuong.eShop.CatalogService.Application.CatalogItems.Queries;
using Phuong.eShop.CatalogService.Application.Common;

namespace Phuong.eShop.CatalogService.Controllers;

[Route("api/catalog/items")]
public class CatalogItemsController(IWebHostEnvironment env) : BaseApiController
{
    [HttpGet]
    public Task<PaginatedList<CatalogItemDto>> Get(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] int brand = 0,
        [FromQuery] int type = 0,
        [FromQuery] string searchString = "")
    {
        return Mediator.Send(new GetCatalogItemWithPaginationQuery
        {
            PageNumber = pageNumber, PageSize = pageSize,
            Brand = brand, Type = type, SearchString = searchString
        });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CatalogItemDto>> GetById(int id) {
        var catalogItem = await Mediator.Send(new GetCatalogItemByIdQuery(id));
        if (catalogItem is null)
        {
            return NotFound();
        }

        return catalogItem;
    }

    [HttpGet("{id:int}/pic")]
    public async Task<IActionResult> GetItemPicture(int id)
    {
        var catalogItem = await Mediator.Send(new GetCatalogItemByIdQuery(id));
        if (catalogItem is null)
        {
            return NotFound();
        }

        var path = Path.Combine(env.ContentRootPath, "Pics", catalogItem.PictureUri);
        return PhysicalFile(path, "image/webp");
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody]CreateCatalogItemCommand request)
    {
        await Mediator.Send(request);
        return Ok("Create Successfully!");
    }
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteById(long id)
    {
        var catalogItem = await Mediator.Send( new DeleteCatalogItemCommand(id));
        if (catalogItem ==0 )
        {
            return NotFound();
        }
        return Ok("Delete Successfully!");
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateById([FromBody] UpdateCatalogItemCommand request, int id)
    {
        request.Id = id;
        var catalogItem = await Mediator.Send(request);
        if (catalogItem == 0)
        {
            return NotFound();
        }
        return Ok("Update Successfully!");
    }
}