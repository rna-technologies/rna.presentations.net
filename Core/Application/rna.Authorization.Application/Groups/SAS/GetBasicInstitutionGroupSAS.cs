using rna.Core.Infrastructure.Extensions;

namespace rna.Authorization.Application.Groups.SAS;

public class GetBasicInstitutionGroupSAS : IRequest<IActionResult>
{

    public class GetBasicInstitutionGroupSASHandler(IServiceProvider serviceProvider) : BaseRequestHandler<GetBasicInstitutionGroupSAS, IActionResult>(serviceProvider)
    {
        public override async Task<IActionResult> Handle(GetBasicInstitutionGroupSAS request, CancellationToken cancellationToken)
        {
            var hello = await Identity.Set<Group>()
                .Where(g => g.Id == Scope.GroupId)
                .Select(g => g.SuperGroup)
                .Map<BasicInstitutionalGroupSasModel>()
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (hello == null)
                this.ThrowException("User's branch does not have a Company or Head Branch");

            return hello.ToOk();
        }
    }
}
