using System;
using System.Threading.Tasks;
using Accounting.Domain.Entities;
using System.Threading;
using System.Linq;
using rna.Authorization.Application.Models;

namespace rna.Authorization.Application
{
    //using Resource.Infrastructure.DbSetModels;
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
                    IsClosed = t.TellerRegisters.Any(r => r.OpenDate.Date == request.Date.Date && r.IsClosed),
                    IsRegistered = t.TellerRegisters.Any(r => r.OpenDate.Date == request.Date.Date),
                    TellerType = t.TellerType.ToString(),
                    UserId = t.UserId
                }));
        }
    }
}
