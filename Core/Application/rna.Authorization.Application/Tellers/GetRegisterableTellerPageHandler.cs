using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using rna.Authorization.Application.Models;
using rna.Core.Identity.Infrastructure.Extensions.RelatedUser;
//using Resource.Infrastructure.DbSetModels;

namespace rna.Authorization.Application
{
    public class GetRegisterableTellerPageHandler : BaseRequestHandler<GetRegisterableTellerPage, PaginationInfo<RegisterableTellerModel>>
    {
        public GetRegisterableTellerPageHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
        public override async Task<PaginationInfo<RegisterableTellerModel>> Handle(GetRegisterableTellerPage request, CancellationToken cancellationToken)
        {

            var tellers = (await Mediator.Send(new GetRegisterableTellerQuery
            {
                Date = request.Date ?? DateTime.Now,
                Params = request.Params
            }, cancellationToken).ConfigureAwait(false))
            .ToList().Map<List<RegisterableTellerModel>>();

            return tellers.GetRelatedUserInfoPage(IdentityService, request.Params);
        }
    }
}
