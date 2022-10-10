using System.Threading;
using System.Threading.Tasks;
using System;
using rna.Authorization.Application.Models;

namespace rna.Authorization.Application
{
    public class GetTellerableUserPageHandler : BaseRequestHandler<GetTellerableUserPage, PaginationInfo<CustomUserModel>>
    {
        public GetTellerableUserPageHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
        public override async Task<PaginationInfo<CustomUserModel>> Handle(GetTellerableUserPage request, CancellationToken cancellationToken)
        {
            var queryable = await Mediator.Send(new GetTellerableUserQuery
            {
                Params = request.Params
            }, cancellationToken).ConfigureAwait(false);

            var pageable = await queryable.ToPageableAsync(IdentityService.DbContext, request.Params);

            return pageable;
        }
    }
}
