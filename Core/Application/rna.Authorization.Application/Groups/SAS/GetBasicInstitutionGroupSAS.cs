using rna.Core.Infrastructure.Extensions;

namespace rna.Authorization.Application.Groups.Sas;

public class GetBasicInstitutionGroupSas : IRequest<IActionResult>
{

    public class GetBasicInstitutionGroupSasHandler(IServiceProvider serviceProvider) : BaseRequestHandler<GetBasicInstitutionGroupSas, IActionResult>(serviceProvider)
    {
        public override async Task<IActionResult> Handle(GetBasicInstitutionGroupSas request, CancellationToken cancellationToken)
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
