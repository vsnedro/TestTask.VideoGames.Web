using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace VideoGames.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class ApiBaseController : ControllerBase
{
    private IMediator _mediator;
    public IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
}
