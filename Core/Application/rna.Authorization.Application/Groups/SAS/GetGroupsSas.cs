using rna.Core.Infrastructure.Extensions;

namespace rna.Authorization.Application.Groups.SAS;

public class GetGroupsSas : IRequest<IActionResult>
{

    public class GetGroupsSasHandler(IServiceProvider serviceProvider) : BaseRequestHandler<GetGroupsSas, IActionResult>(serviceProvider)
    {
        public override async Task<IActionResult> Handle(GetGroupsSas request, CancellationToken cancellationToken)
        {
            //var companyGroups2 = await Identity.Set<Group>()
            //    .Where(g => g.Id == Scope.GroupId)
            //    .Select(g => g.SuperGroup)
            //    .SelectMany(g => g.InverseSuperGroup)
            //    .Map<BasicGroupSasModel>()
            //    .ToListAsync();


            var superGroup = await Identity.Set<Group>()
                .Where(g => g.Id == Scope.GroupId)
                .Select(g => new { Id = g.SuperGroupId }).FirstOrDefaultAsync();

            if (superGroup == null) this.ThrowException("User's branch has no Head branch or company");

            var companyGroups = Identity.Set<Group>().Where(g => g.SuperGroupId == superGroup.Id)
                .Map<BasicGroupSasModel>()
                .ToListAsync();

            return companyGroups.ToOk();
        }
    }
}
