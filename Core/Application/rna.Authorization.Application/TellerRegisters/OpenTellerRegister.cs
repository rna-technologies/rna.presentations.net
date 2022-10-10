using System;
using MediatR;
using rna.Core.Identity.Domain;
using rna.Core.Infrastructure.BaseHandlers;
using rna.Core.Resource.Infrastructure.Services;
using rna.Exceptions.Extensions;
using rna.Filters.Extensions;
using rna.Filters.Models;

namespace rna.Authorization.Application.TellerRegisters
{
    public class OpenTellerRegister : IRequest<Unit>
    {
        public DateTime? Date { get; set; }
        public int TellerId { get; set; }
        public required UrlQueryParams Params { get; set; }
    }

    public class OpenTellerRegisterHandler : BaseRequestHandler<OpenTellerRegister, Unit>
    {
        public OpenTellerRegisterHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
        public override async Task<Unit> Handle(OpenTellerRegister request, CancellationToken cancellationToken)
        {
            request.Date ??= DateTime.Now;


            var isOpened = ResourceService.Entity<TellerRegister>()
                .Any(r => r.TellerId == request.TellerId && r.GroupId == SelectedGroupId && r.CloseDate == null);

            if (isOpened) request.ThrowException("Teller has already been Registered. Please close the previous register");


            var register = new TellerRegister
            {
                OpenDate = request.Date.Value,
                GroupId = SelectedGroupId!.Value,
                TellerId = request.TellerId,
                RegistererId = LoggedUserId
            };

            await ResourceService
                .CreateAsync(register)
                .ConfigureAwait(false);

            return Unit.Value;
        }
    }

}
