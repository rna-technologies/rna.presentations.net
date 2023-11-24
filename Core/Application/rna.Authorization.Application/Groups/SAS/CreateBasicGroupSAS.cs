using rna.Core.Infrastructure.Logics.Users.Models.EditModels;
using rna.Core.Identity.Domain;
using rna.Core.Infrastructure.Logics.Users.DefaultUsers.Helpers;
using rna.Authorization.Validator;
using rna.Authorization.Application.Models;

namespace rna.Authorization.Application.Groups.SAS;

/// <summary>
/// Institutional Group or Branch can only be created by a user if that user is already a part of a branch i.e. HQ
/// This means Super Group, Group Location, and Group Profile, and also a sub Group like HQ must have already been created
/// Note that Company is a Super Group and a HQ is a sub Group of Company
/// </summary>
public class CreateBasicGroupSAS : IRequest<Unit>
{
    public required BasicGroupSasModel Model { get; set; } = null!;

    public class CreateBasicGroupSASHandler(IServiceProvider serviceProvider) : BaseRequestHandler<CreateBasicGroupSAS, Unit>(serviceProvider)
    {
        public override async Task<Unit> Handle(CreateBasicGroupSAS request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            model.ThrowArgumentExceptionFor(r => r.Name is null or "", "Please specify a 'Name'");

            model.Description = model.Description?.Trim() is null or "" ? model.Name : model.Description;

            model.Type = model.Type.IsNullOrEmptyTrimmed() ? null : model.Type;

            var superGroupInfo = Identity.Set<Group>()
                .Where(g => g.Id == Scope.GroupId)
                .Select(g => new
                {
                    g.SuperGroupId,
                    g.SuperGroup!.GroupProfileId,
                    g.SuperGroup!.GroupLocationId
                }).FirstOrDefault();

            if (superGroupInfo is null) this.ThrowException("No group or branch was found for the user");

            superGroupInfo.ThrowArgumentExceptionFor(i => i.SuperGroupId is null or 0, "No Company or Instituation is found for User");


            Group group = new()
            {
                AppId = Scope.AppId,
                Description = request.Model.Name,
                GroupLocationId = superGroupInfo.GroupLocationId,
                GroupProfileId = superGroupInfo.GroupProfileId,
                Name = request.Model.Name,
                Type = model.Type ?? GroupTypeSAS.Branch.ToString(),
                SuperGroupId = superGroupInfo.SuperGroupId
            };

            await Identity.CreateAsync(group).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}
