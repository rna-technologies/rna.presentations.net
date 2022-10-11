namespace rna.Authorization.Application.Tellers;

//using Resource.Infrastructure.DbSetModels;
public class GetRegisterableTellerQuery : IRequest<IQueryable<RegisterableTellerModel>>
{
    public DateTime Date { get; set; }
}

public class GetRegisterableTellerQueryHandler : BaseRequestHandler<GetRegisterableTellerQuery, IQueryable<RegisterableTellerModel>>
{
    public GetRegisterableTellerQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
    public override Task<IQueryable<RegisterableTellerModel>> Handle(GetRegisterableTellerQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(ResourceService.Entity<Teller>()
            .Where(t => !t.IsDeleted)
            .Select(t => new RegisterableTellerModel
            {
                Id = t.Id,
                IsDeleted = t.IsDeleted,
                RegisteredDate = t.RegisteredDate,
                IsClosed = t.TellerRegisters.Any(r => r.OpenDate.Date == request.Date.Date && r.CloseDate != null),
                IsRegistered = t.TellerRegisters.Any(r => r.OpenDate.Date == request.Date.Date),
                TellerType = t.TellerType.ToString(),
                UserId = t.UserId
            }));
    }
}
