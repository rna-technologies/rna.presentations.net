namespace rna.Authorization.Application.Tellers;

public class CreateTeller : IRequest<Unit>
{
    public required string UserId { get; set; }
    public int? AppId { get; set; }
}
public class CreateTellerHandler : BaseRequestHandler<CreateTeller, Unit>
{
    public CreateTellerHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
    public override async Task<Unit> Handle(CreateTeller request, CancellationToken cancellationToken)
    {
        request.ThrowArgumentExceptionFor(r => r.UserId == null);

        var teller = Identity.Set<Teller>().FirstOrDefault(t => t.AppId == (request.AppId ?? Scope.AppId) && t.UserId == request.UserId);

        if (teller is not null)
        {
            if (teller.IsDeleted == false) request.ThrowException("The User has already been Registered as a Teller.");
            teller.IsDeleted = false;
            Identity.Update(teller);
            return Unit.Value;
        }


        teller = new Teller { RegisteredDate = DateTime.Now, UserId = request.UserId, AppId = request.AppId ?? Scope.AppId };

        await Identity.CreateAsync(teller).ConfigureAwait(false);

        return Unit.Value;
    }
}
