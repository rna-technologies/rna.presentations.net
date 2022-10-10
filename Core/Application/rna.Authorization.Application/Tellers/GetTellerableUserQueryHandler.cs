using Accounting.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq;
using rna.Core.Infrastructure.Logics.Users.DefaultUsers;
using rna.Authorization.Application.Models;

namespace rna.Authorization.Application
{
    public class GetTellerableUserQueryHandler : BaseRequestHandler<GetTellerableUserQuery, IQueryable<CustomUserModel>>
    {
        public GetTellerableUserQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
        public override async Task<IQueryable<CustomUserModel>> Handle(GetTellerableUserQuery request, CancellationToken cancellationToken)
        {

            var tellerUserIds = ResourceService.Entity<Teller>()
                .Where(t => !t.IsDeleted).Select(t => t.UserId)
                .ToArray();


            var users = (await Mediator.Send(new GetDefaultUserQuery { }, cancellationToken).ConfigureAwait(false))
                .Where(u => u.IsTellerable)
                .WhereNotAny(tellerUserIds, u => u.Id)
                .Select(u => new CustomUserModel
                {
                    Email = u.Email,
                    FullName = u.FullName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    MiddleName = u.MiddleName,
                    Id = u.Id,
                    PhoneNumber = u.PhoneNumber,
                    Photo = "",
                });

            return users;
        }
    }
}
