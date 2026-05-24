using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Api.Controllers
{
    public abstract class AppControllerBase : ControllerBase {
        protected Guid? UserId => User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value is string id ? Guid.Parse(id) : null;
    }
}