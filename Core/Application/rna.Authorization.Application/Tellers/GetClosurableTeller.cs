using rna.Authorization.Application.Models;
using MediatR;
using System;
using rna.Filters.Models;
using rna.Exceptions.Extensions;
using rna.Core.Infrastructure.BaseHandlers;
using rna.Core.Identity.Domain;
using rna.Filters.Extensions;

namespace rna.Authorization.Application
{
    public class GetClosurableTeller : IRequest<ClosurableTellerModel>
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int AccountTypeId { get; set; }
        public int GroupId { get; set; }
        public int SubscriptionTypeId { get; set; }
        public UrlQueryParams Params { get; set; }
    }
    public class GetClosurableTellerHandler : BaseRequestHandler<GetClosurableTeller, ClosurableTellerModel>
    {
        public GetClosurableTellerHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
        public override async Task<ClosurableTellerModel> Handle(GetClosurableTeller request, CancellationToken cancellationToken)
        {
            request.ThrowArgumentExceptionFor(r => r.Id == 0);

            var queryable = await Mediator.Send(new GetClosurableTellerQuery
            {
                Date = request.Date,
                AccountTypeId = request.AccountTypeId,
                GroupId = request.GroupId,
                SubscriptionTypeId = request.SubscriptionTypeId,
                Params = request.Params
            }, cancellationToken).ConfigureAwait(false);

            var teller = await queryable.FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken: cancellationToken).ConfigureAwait(false);

            var user = IdentityService.Set<User>()
                .Where(u => u.Id == teller.UserId).Select(u => new
                {
                    u.FullName,
                    u.PhoneNumber,
                    u.Email,
                }).FirstOrDefault();
            teller.Email = user.Email;
            teller.FullName = user.FullName;
            teller.PhoneNumber = user.PhoneNumber;

            return teller;
        }
    }

}
