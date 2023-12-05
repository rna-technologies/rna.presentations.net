using rna.Core.Resource.Infrastructure;

namespace rna.Authorization.Application.Tellers;

//using Resource.Infrastructure.DbSetModels;
public class GetRegisterableTellerPage : IRequest<PaginationInfo<RegisterableTellerModel>>
{
    public DateTime? Date { get; set; }
    public UrlQueryParams Params { get; set; }
}

public class GetRegisterableTellerPageHandler : BaseRequestHandler<GetRegisterableTellerPage, PaginationInfo<RegisterableTellerModel>>
{
    public GetRegisterableTellerPageHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
    public override async Task<PaginationInfo<RegisterableTellerModel>> Handle(GetRegisterableTellerPage request, CancellationToken cancellationToken)
    {

        var tellers = (await Mediator.Send(new GetRegisterableTellerQuery
        {
            Date = request.Date ?? DateTime.Now,
        }, cancellationToken).ConfigureAwait(false))
        .Map<RegisterableTellerModel>().ToList();

        return tellers.GetUserInfoPage(Identity, request.Params);
    }
}
