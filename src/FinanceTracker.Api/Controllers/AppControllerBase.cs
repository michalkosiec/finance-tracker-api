using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class AppControllerBase(ISender mediator) : ControllerBase
    {
        protected readonly ISender Mediator = mediator;

        protected Guid CurrentUserId
        {
            get
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                return Guid.TryParse(userIdClaim, out var id) ? id : Guid.Empty;
            }
        }
    }
}
