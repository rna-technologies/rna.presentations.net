using System;
using System.Threading.Tasks;
using MediatR;
using Accounting.Domain.Entities;
using System.Threading;
using System.Linq;

namespace rna.Authorization.Application
{
    public class VerifyIsOpenedTellerHandler : BaseRequestHandler<VerifyIsOpenedTeller, Unit>
    {
        public VerifyIsOpenedTellerHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
        public override Task<Unit> Handle(VerifyIsOpenedTeller request, CancellationToken cancellationToken)
        {
            request.ThrowArgumentExceptionFor(r => r.UserId == null);

            if (Configuration.VerifyIsPowerUser(LoggedUserId)) return Unit.Task;

            var verified = request.OpenDate is null ?
                ResourceService.Entity<TellerRegister>()
                .Any(p => p.Teller.UserId == request.UserId && !p.IsClosed) :
                ResourceService.Entity<TellerRegister>()
                .Any(p => p.Teller.UserId == request.UserId && p.OpenDate.Date == request.OpenDate.Value.Date && !p.IsClosed);

            if (!verified && !request.ThrowExceptionIfVerified) request.ThrowException("Teller registeration has been closed or has not been registered");

            if (verified && request.ThrowExceptionIfVerified) request.ThrowException("Teller has already been opened or registered");

            return Unit.Task;
        }
    }
}
