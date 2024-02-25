using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Shared.Response.Abstract;
using Ecommerce.Shared.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public UserController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("change-password")]
        public async Task<ActionResult<IResponse>> ChangePassword([FromBody] UserChangePassword changePassword)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _identityService.ChangePassword(userId, changePassword.CurrentPassword, changePassword.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
