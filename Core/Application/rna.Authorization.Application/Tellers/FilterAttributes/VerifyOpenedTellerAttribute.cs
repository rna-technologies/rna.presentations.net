using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace rna.Authorization.Application.FilterAttributes
{
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
    
}
