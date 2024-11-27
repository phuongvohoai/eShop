using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Phuong.eShop.ServiceDefaults.AspNetCore;

namespace Phuong.eShop.Identity.Controllers
{
    [Route("api/me")]

    public class UserController: BaseApiController
    {
        [HttpGet]
        public string? Get()
        {
            var user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return user?.Value;
        }
    }
}
