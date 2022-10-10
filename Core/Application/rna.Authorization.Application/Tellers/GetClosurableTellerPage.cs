using MediatR;
using System;
using rna.Authorization.Application.Models;

namespace rna.Authorization.Application
{
    public class GetClosurableTellerPage : IRequest<PaginationInfo<ClosurableTellerModel>>
    {
        public DateTime Date { get; set; }
        public int AccountTypeId { get; set; }
        public int GroupId { get; set; }
        public int SubscriptionTypeId { get; set; }
        public UrlQueryParams Params { get; set; }
    }

    public class GetClosurableTellerPageHandler : BaseRequestHandler<GetClosurableTellerPage, PaginationInfo<ClosurableTellerModel>>
    {
        public GetClosurableTellerPageHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
        public override async Task<PaginationInfo<ClosurableTellerModel>> Handle(GetClosurableTellerPage request, CancellationToken cancellationToken)
        {
            var tellers = (await Mediator.Send(new GetClosurableTellerQuery
            {
                Date = request.Date,
                AccountTypeId = request.AccountTypeId,
                GroupId = request.GroupId,
                SubscriptionTypeId = request.SubscriptionTypeId,
                Params = request.Params
            }, cancellationToken).ConfigureAwait(false))
            .ToList();

            var param = request.Params;

            var users = IdentityService.Entity<User>()
                .FindAny(tellers.Select(t => t.UserId).Distinct().ToList(), u => u.Id)
                .Get(param.SearchValue, param.SearchFields)
                .Select(u => new
                {
                    u.Id,
                    u.FullName,
                    u.PhoneNumber,
                    u.Email,
                }).ToList();


            return GetTellerModels()
                .ToList()
                .ToPageable(param);

            IEnumerable<ClosurableTellerModel> GetTellerModels()
            {
                foreach (var teller in tellers)
                {
                    var user = users.FirstOrDefault(u => u.Id == teller.UserId);

                    if (user is null) continue;

                    teller.Email = user?.Email;
                    teller.FullName = user?.FullName;
                    teller.PhoneNumber = user?.PhoneNumber;

                    yield return teller;
                }
            }

        }
    }
}
