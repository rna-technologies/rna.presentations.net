namespace rna.Authorization.Application.Tellers;

public class DeleteTeller : IRequest<Unit>
{
    public required string UserId { get; set; }
    public int? AppId { get; set; }
}
public class DeleteTellerHandler : BaseRequestHandler<DeleteTeller, Unit>
{
    public DeleteTellerHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
    public override async Task<Unit> Handle(DeleteTeller request, CancellationToken cancellationToken)
    {
        var teller = Identity.Set<Teller>()
            .FirstOrDefault(t =>
            t.AppId == (request.AppId ?? Scope.AppId) &&
            t.UserId == request.UserId &&
            (!t.TellerRegisters.Any() || t.TellerRegisters.All(r => r.CloseDate != null)));

        if (teller is null) request.ThrowException("Please 'close' teller for the day before deleting. Else ensure the user is available");

        teller.IsDeleted = true;

        await Identity.UpdateAsync(teller).ConfigureAwait(false);

        return Unit.Value;
    }
}
