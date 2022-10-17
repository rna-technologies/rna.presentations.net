using rna.Core.Infrastructure.Extensions.Authorization;

namespace rna.Authorization.Application.Tellers;

public class VerifyIsOpenedTeller : IRequest<Unit>
{
    public string UserId { get; set; }
    public DateTime? OpenDate { get; set; }
    public bool ThrowExceptionIfVerified { get; set; } = false;
}
public class VerifyIsOpenedTellerHandler : BaseRequestHandler<VerifyIsOpenedTeller, Unit>
{
    public VerifyIsOpenedTellerHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
    public override Task<Unit> Handle(VerifyIsOpenedTeller request, CancellationToken cancellationToken)
    {
        request.ThrowArgumentExceptionFor(r => r.UserId == null);

        if (Configuration.VerifyIsPowerUser(LoggedUserId)) return Unit.Task;

        var verified = request.OpenDate is null ?
            Identity.Set<TellerRegister>()
            .Any(p => p.Teller.UserId == request.UserId && p.CloseDate == null) :
            Identity.Set<TellerRegister>()
            .Any(p => p.Teller.UserId == request.UserId && p.OpenDate.Date == request.OpenDate.Value.Date && p.CloseDate == null);

        if (!verified && !request.ThrowExceptionIfVerified) request.ThrowException("Teller registeration has been closed or has not been registered");

        if (verified && request.ThrowExceptionIfVerified) request.ThrowException("Teller has already been opened or registered");

        return Unit.Task;
    }
}
