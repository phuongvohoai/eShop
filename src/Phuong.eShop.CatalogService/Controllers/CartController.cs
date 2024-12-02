using Phuong.eShop.CatalogService.Application.Carts.Commands;

using Phuong.eShop.CatalogService.Application.Carts.Queries;

namespace Phuong.eShop.CatalogService.Controllers
{
    [Route("api/cart")]
    public class CartController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> SaveCart([FromBody] CreateCartCommand request)
        {
            await Mediator.Send(request);
            return Ok("OKI");
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartItemDto>> GetById(int id)
        {
            var cartItem = await Mediator.Send(new GetCartItemsQuery()
            {
                UserId = id
            });
            if (cartItem is null)
            {
                return NotFound();
            }
            return cartItem;
        }
    }
}
