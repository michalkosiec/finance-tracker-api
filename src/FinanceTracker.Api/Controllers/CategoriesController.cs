using FinanceTracker.Application.Features.Categories.Commands.CreateCategory;
using FinanceTracker.Application.Features.Categories.Commands.DeleteCategory;
using FinanceTracker.Application.Features.Categories.Commands.UpdateCategory;
using FinanceTracker.Application.Features.Categories.Queries.GetCategories;
using FinanceTracker.Application.Features.Categories.Queries.GetCategoryById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Api.Controllers
{
    [Authorize]
    public class CategoriesController(ISender mediator) : AppControllerBase(mediator)
    {
        [HttpGet]
        public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(
                new GetCategoriesQuery(CurrentUserId),
                cancellationToken
            );

            return Ok(result);
        }

        [HttpGet("{id:guid}", Name = "GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(
            Guid id,
            CancellationToken cancellationToken
        )
        {
            var result = await Mediator.Send(
                new GetCategoryByIdQuery(CurrentUserId, id),
                cancellationToken
            );

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(
            [FromBody] CreateCategoryRequest request,
            CancellationToken cancellationToken
        )
        {
            var result = await Mediator.Send(
                new CreateCategoryCommand(CurrentUserId, request.Name, request.Icon, request.Color),
                cancellationToken
            );

            return CreatedAtAction(nameof(GetCategoryById), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCategory(
            Guid id,
            [FromBody] UpdateCategoryRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new UpdateCategoryCommand(
                id,
                CurrentUserId,
                request.Name,
                request.Icon,
                request.Color
            );

            await Mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCategory(
            Guid id,
            CancellationToken cancellationToken
        )
        {
            await Mediator.Send(new DeleteCategoryCommand(CurrentUserId, id), cancellationToken);

            return NoContent();
        }

        public record CreateCategoryRequest(string Name, string Icon, string Color);

        public record UpdateCategoryRequest(string Name, string Icon, string Color);
    }
}
