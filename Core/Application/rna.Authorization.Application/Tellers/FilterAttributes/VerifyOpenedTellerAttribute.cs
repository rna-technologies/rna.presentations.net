using Microsoft.AspNetCore.Mvc.Filters;

namespace rna.Authorization.Application.Tellers;

public class VerifyOpenedTellerAttribute : Attribute, IAsyncActionFilter
{
    public IMediator Mediator { get; }
    public VerifyOpenedTellerAttribute(IMediator mediator)
    {
        Mediator = mediator;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        await Mediator.Send(new VerifyIsOpenedLoggedTeller
        {
            OpenDate = DateTime.Now,
        }).ConfigureAwait(false);

        await next();
    }
}

