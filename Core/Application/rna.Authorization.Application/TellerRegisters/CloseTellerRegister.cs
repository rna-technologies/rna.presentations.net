namespace rna.Authorization.Application.TellerRegisters;

public class CloseTellerRegister : IRequest<Unit>
{
    public int TellerId { get; set; }
}

public class CloseTellerRegisterHandler : BaseRequestHandler<CloseTellerRegister, Unit>
{
    public CloseTellerRegisterHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
    public override Task<Unit> Handle(CloseTellerRegister request, CancellationToken cancellationToken)
    {
        request.ThrowArgumentExceptionFor(r => r.TellerId == 0);

        var registers = Identity.Set<TellerRegister>()
            .AsNoTracking()
            .Where(r => r.TellerId == request.TellerId && r.GroupId == SelectedGroupId && r.CloseDate == null)
            .ToList();

        foreach (var register in registers)
        {
            register.CloseDate = DateTime.Now;
            register.CloserId = LoggedUserId;
        }

        Identity.UpdateRange(registers);

        return Unit.Task;
    }
}

