using rna.Authorization.Application.Models;
using MediatR;
using rna.Filters.Models;
using rna.Core.Resource.Infrastructure.Services;
using rna.Core.Infrastructure.BaseHandlers;
using rna.Core.Identity.Infrastructure.Extensions;
using rna.Filters.Extensions;
using rna.Core.Identity.Domain;

namespace rna.Authorization.Application
{
    public class GetClosurableTellerByRegisterPage : IRequest<PaginationInfo<ClosurableTellerModel>>
    {
        public UrlQueryParams Params { get; set; }
    }
    public class GetClosurableTellerByRegisterPageHandler : BaseRequestHandler<GetClosurableTellerByRegisterPage, PaginationInfo<ClosurableTellerModel>>
    {
        public GetClosurableTellerByRegisterPageHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
        public override Task<PaginationInfo<ClosurableTellerModel>> Handle(GetClosurableTellerByRegisterPage request, CancellationToken cancellationToken)
        {
            var groupIds = IdentityService.GetChildrenGroups(SelectedGroupId.Value, true).Select(g => g.Id).ToList();
            var result = IdentityService.Set<TellerRegister>()
                .Where(r => r.CloseDate == null)
                .FindAny(groupIds, r => r.GroupId)
                .Select(g => new ClosurableTellerModel
                {
                    CashFloatTotal = g.CashFloatTotal,
                    CloseDate = g.CloseDate,
                    Email = string.Empty,
                    FullName = string.Empty,
                    Id = g.TellerId,
                    IsClosed = g.IsClosed,
                    OpenDate = g.OpenDate,
                    //PaymentTotal = g.Transactions.Sum(t => t.UnTaxedNetCost + t.TotalTaxSum),

                    Debt = g.Transactions
                    .Where(t => !t.Reversed && t.ActivationStatus.Subscription.TransactionAccount.TransactionFactor.IncomeFactor > 0)
                    .Sum(t => t.UnTaxedNetCost + t.TotalTaxSum),

                    Credit = g.Transactions
                    .Where(t => !t.Reversed && t.ActivationStatus.Subscription.TransactionAccount.TransactionFactor.IncomeFactor < 0)
                    .Sum(t => t.UnTaxedNetCost + t.TotalTaxSum),

                    TransactionPayment = 0,
                    PhoneNumber = string.Empty,
                    Photo = string.Empty,
                    //SubscriptionTypeName = g.SubscriptionTypeName,
                    TellerRegisterId = g.Id,
                    UserId = g.Teller.UserId
                }).ToList()
                .GetRelatedUserInfoPage(IdentityService, request.Params);

            return Task.FromResult(result);
        }
    }

}
