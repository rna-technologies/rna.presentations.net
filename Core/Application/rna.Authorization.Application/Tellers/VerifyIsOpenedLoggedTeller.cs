namespace rna.Authorization.Application.Tellers;

public class VerifyIsOpenedLoggedTeller : IRequest<Unit>
{
    public DateTime? OpenDate { get; set; }
}
public class VerifyIsOpenedLoggedTellerHandler : BaseRequestHandler<VerifyIsOpenedLoggedTeller, Unit>
{
    public VerifyIsOpenedLoggedTellerHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
    public override async Task<Unit> Handle(VerifyIsOpenedLoggedTeller request, CancellationToken cancellationToken)
    {
        await Mediator.Send(new VerifyIsOpenedTeller
        {
            OpenDate = DateTime.Now,
            UserId = LoggedUserId,
            ThrowExceptionIfVerified = false,
        }, cancellationToken).ConfigureAwait(false);

        return Unit.Value;
    }
}
