using MediatR;
using Microsoft.EntityFrameworkCore;
using rna.Core.Identity.Domain;
using rna.Core.Infrastructure.BaseHandlers;
using rna.Core.Resource.Infrastructure.Services;
using rna.Exceptions.Extensions;
using rna.Filters.Extensions;
using rna.Filters.Models;

namespace rna.Authorization.Application.TellerRegisters
{
    public class CloseTellerRegister : IRequest<Unit>
    {
        public int TellerId { get; set; }
        public decimal MoneyAtHand { get; set; }
        //public UrlQueryParams Params { get; set; }
    }

    public class CloseTellerRegisterHandler : BaseRequestHandler<CloseTellerRegister, Unit>
    {
        public CloseTellerRegisterHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
        public override Task<Unit> Handle(CloseTellerRegister request, CancellationToken cancellationToken)
        {
            request.ThrowArgumentExceptionFor(r => r.TellerId == 0);

            var tellerRegisterSet = IdentityService.Set<TellerRegister>();

            var registers = tellerRegisterSet
                .AsNoTracking()
                .Where(r => r.TellerId == request.TellerId && r.GroupId == SelectedGroupId && r.CloseDate == null)
                .ToList();

            foreach (var register in registers)
            {
                register.CloseDate = DateTime.Now;
                register.CloserId = LoggedUserId;
            }


            ResourceService.UpdateRangeWithoutSaving(registers);

            ResourceService.DbContext.SaveChanges();

            return Unit.Task;
        }
    }

}
