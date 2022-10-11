namespace rna.Authorization.Application.Tellers;

public class GetTellerableUser : IRequest<UserModel>
{
    public string UserId { get; set; }
    public UrlQueryParams Params { get; set; }
}
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
