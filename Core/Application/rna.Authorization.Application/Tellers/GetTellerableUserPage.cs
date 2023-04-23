namespace rna.Authorization.Application.Tellers;

public class GetTellerableUserPage : IRequest<PaginationInfo<CustomUserModel>>
{
    public required UrlQueryParams Params { get; set; }
}
public class GetTellerableUserPageHandler : BaseRequestHandler<GetTellerableUserPage, PaginationInfo<CustomUserModel>>
{
    public GetTellerableUserPageHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
    public override async Task<PaginationInfo<CustomUserModel>> Handle(GetTellerableUserPage request, CancellationToken cancellationToken)
    {
        var queryable = await Mediator.Send(new GetTellerableUserQuery(), cancellationToken).ConfigureAwait(false);

        var pageable = await queryable.ToPageableAsync(Identity.DbContext(), request.Params);

        return pageable;
    }
}
