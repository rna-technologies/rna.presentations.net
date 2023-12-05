using rna.Core.Resource.Infrastructure;

namespace rna.Authorization.Application.Tellers;

public class GetClosurableTellerByRegisterPage : IRequest<PaginationInfo<ClosurableTellerModel>>
{
    public int? AppId { get; set; }
    public UrlQueryParams Params { get; set; }
}
public class GetClosurableTellerByRegisterPageHandler : BaseRequestHandler<GetClosurableTellerByRegisterPage, PaginationInfo<ClosurableTellerModel>>
{
    public GetClosurableTellerByRegisterPageHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
    public override Task<PaginationInfo<ClosurableTellerModel>> Handle(GetClosurableTellerByRegisterPage request, CancellationToken cancellationToken)
    {
        var groupIds = Identity.GetChildrenGroups(Scope.GroupId, true).Select(g => g.Id).ToList();
        var result = Identity.Set<TellerRegister>()
            .Where(r => r.Teller.AppId == (request.AppId ?? Scope.AppId))
            .Where(r => r.CloseDate == null)
            .FindAny(groupIds, r => r.GroupId)
            .Select(g => new ClosurableTellerModel
            {
                //CashFloatTotal = g.CashFloatTotal,
                CloseDate = g.CloseDate,
                Email = string.Empty,
                FullName = string.Empty,
                Id = g.TellerId,
                IsClosed = g.CloseDate != null,
                OpenDate = g.OpenDate,
                //PaymentTotal = g.Transactions.Sum(t => t.UnTaxedNetCost + t.TotalTaxSum),

                //Debt = g.Transactions
                //.Where(t => !t.Reversed && t.ActivationStatus.Subscription.TransactionAccount.TransactionFactor.IncomeFactor > 0)
                //.Sum(t => t.UnTaxedNetCost + t.TotalTaxSum),

                //Credit = g.Transactions
                //.Where(t => !t.Reversed && t.ActivationStatus.Subscription.TransactionAccount.TransactionFactor.IncomeFactor < 0)
                //.Sum(t => t.UnTaxedNetCost + t.TotalTaxSum),

                //TransactionPayment = 0,
                PhoneNumber = string.Empty,
                Photo = string.Empty,
                //SubscriptionTypeName = g.SubscriptionTypeName,
                TellerRegisterId = g.Id,
                UserId = g.Teller.UserId
            }).ToList()
            .GetUserInfoPage(Identity, request.Params);

        return Task.FromResult(result);
    }
}
