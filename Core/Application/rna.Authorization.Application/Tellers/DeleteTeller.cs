namespace rna.Authorization.Application.Tellers;

public class DeleteTeller : IRequest<Unit>
{
    public string UserId { get; set; }
    public UrlQueryParams Params { get; set; }
}
public class DeleteTellerHandler : BaseRequestHandler<DeleteTeller, Unit>
{
    public DeleteTellerHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
    public override async Task<Unit> Handle(DeleteTeller request, CancellationToken cancellationToken)
    {
        var teller = ResourceService.Entity<Teller>().FirstOrDefault(t => t.UserId == request.UserId && (!t.TellerRegisters.Any() || t.TellerRegisters.All(r => r.CloseDate != null)));

        if (teller is null) request.ThrowException("Please 'close' teller for the day before deleting. Else ensure the user is available");

        teller.IsDeleted = true;

        await ResourceService.UpdateAsync(teller).ConfigureAwait(false);

        return Unit.Value;
    }
}
