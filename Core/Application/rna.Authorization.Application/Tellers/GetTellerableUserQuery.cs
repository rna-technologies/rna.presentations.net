using rna.Core.Infrastructure.Logics.Users.DefaultUsers;

namespace rna.Authorization.Application.Tellers;

public class GetTellerableUserQuery : IRequest<IQueryable<CustomUserModel>>
{
    public int? AppId { get; set; } = null;
}

public class GetTellerableUserQueryHandler : BaseRequestHandler<GetTellerableUserQuery, IQueryable<CustomUserModel>>
{
    public GetTellerableUserQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
    public override async Task<IQueryable<CustomUserModel>> Handle(GetTellerableUserQuery request, CancellationToken cancellationToken)
    {
        var users = (await Mediator.Send(new GetDefaultUserQuery
        {
            AppId = request.AppId
        }, cancellationToken).ConfigureAwait(false))
        .Where(u => u.IsTellerable && u.Tellers.Where(t => t.AppId == request.AppId).Any() == false)
        .Select(u => new CustomUserModel
        {
            Email = u.Email ?? string.Empty,
            FullName = u.FullName,
            FirstName = u.FirstName,
            LastName = u.LastName,
            MiddleName = u.MiddleName,
            Id = u.Id,
            PhoneNumber = u.PhoneNumber ?? string.Empty,
            Photo = "",
        });

        //var tellerUserIds = ResourceService.Entity<Teller>()
        //    .Where(t => !t.IsDeleted).Select(t => t.UserId)
        //    .ToArray();


        //var users = (await Mediator.Send(new GetDefaultUserQuery { AppId = request.AppId }, cancellationToken).ConfigureAwait(false))
        //    .Where(u => u.IsTellerable)
        //    .WhereNotAny(tellerUserIds, u => u.Id)
        //    .Select(u => new CustomUserModel
        //    {
        //        Email = u.Email ?? string.Empty,
        //        FullName = u.FullName,
        //        FirstName = u.FirstName,
        //        LastName = u.LastName,
        //        MiddleName = u.MiddleName,
        //        Id = u.Id,
        //        PhoneNumber = u.PhoneNumber ?? string.Empty,
        //        Photo = "",
        //    });

        return users;
    }
}
