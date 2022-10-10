using MediatR;

using rna.Core.Resource.Infrastructure.Services;
using rna.Exceptions.Extensions;

namespace rna.Authorization.Application
{
    public class CreateTeller : IRequest<Unit>
    {
        public required string UserId { get; set; }
    }

    public class CreateTellerHandler : BaseRequestHandler<CreateTeller, Unit>
    {
        public CreateTellerHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
        public override async Task<Unit> Handle(CreateTeller request, CancellationToken cancellationToken)
        {
            request.ThrowArgumentExceptionFor(r => r.UserId == null);

            // this.GetData();

            var tellerRepo = ResourceService.Entity<Teller>();
            var teller = tellerRepo.Get().FirstOrDefault(t => t.UserId == request.UserId);


            if (teller is Teller)
            {
                if (teller.IsDeleted == false) request.ThrowException("The User has already been Registered as a Teller.");
                teller.IsDeleted = false;
                tellerRepo.Update(teller);
                return Unit.Value;
            }


            teller = new Teller { RegisteredDate = DateTime.Now, UserId = request.UserId };

            teller = await ResourceService.CreateAsync(teller).ConfigureAwait(false);

            return Unit.Value;
        }
    }

}
