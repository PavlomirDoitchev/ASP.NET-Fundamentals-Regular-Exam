using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace RecipeSharingPlatform.Web.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        protected bool IsUserAuthenticated() => this.User.Identity?.IsAuthenticated ?? false;

        protected string? GetUserId()
        {
            string? userId = null;
            bool isUserAuthenticated = this.IsUserAuthenticated();
            if (isUserAuthenticated)
            {
                userId = this.User
                    .FindFirstValue(ClaimTypes.NameIdentifier);
            }
            return userId;
        }
    }
}
