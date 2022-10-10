using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace rna.Authorization.Application
{
    public class GetTellerableUserHandler : BaseRequestHandler<GetTellerableUser, UserModel>
    {
        public GetTellerableUserHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
        public override async Task<UserModel> Handle(GetTellerableUser request, CancellationToken cancellationToken)
        {
            request.ThrowArgumentExceptionFor(r => r.UserId == null);
            var queryable = await Mediator.Send(new GetTellerableUserQuery
            {
                Params = request.Params
            }, cancellationToken).ConfigureAwait(false);
            var result = queryable.FirstOrDefault(b => b.Id == request.UserId);
            return result;
        }
    }
}
