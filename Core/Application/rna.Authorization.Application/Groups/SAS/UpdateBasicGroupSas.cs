namespace rna.Authorization.Application.Groups.Sas;

public class UpdateBasicGroupSas : IRequest<IActionResult>
{
    public required BasicGroupSasModel Model { get; set; } = null!;

    public class UpdateBasicGroupSasHandler(IServiceProvider serviceProvider) : BaseRequestHandler<UpdateBasicGroupSas, IActionResult>(serviceProvider)
    {
        public override async Task<IActionResult> Handle(UpdateBasicGroupSas request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            model.ThrowArgumentExceptionFor(r => r.Id is 0, "Please specify a branch");

            model.ThrowArgumentExceptionFor(r => r.Name is null or "", "Please specify a branch Name");

            model.Description = model.Description?.Trim() is null or "" ? model.Name : model.Description;

            model.Type = model.Type.IsNullOrEmptyTrimmed() ? null : model.Type;

            var superGroupInfo = Identity.Set<Group>()
                .AsNoTracking()
                .Where(g => g.Id == Scope.GroupId)
                .Select(g => new
                {
                    g.SuperGroupId,
                    g.SuperGroup!.GroupProfileId,
                    g.SuperGroup!.GroupLocationId
                }).FirstOrDefault();

            if (superGroupInfo is null) this.ThrowException("No group or branch was found for the user");

            superGroupInfo.ThrowArgumentExceptionFor(i => i.SuperGroupId is null or 0, "No Company or Instituation is found for User");

            int modelGroupId = 0, superGroupId = 0;
            Group? group = Identity.Set<Group>()
                 .Where(g => g.Id == model.Id && g.SuperGroupId == superGroupInfo.SuperGroupId)
                 .FirstOrDefault();

            if (group is null)
                this.ThrowException("The selected branch is not found under your Company or Institution");


            modelGroupId = model.Id;
            superGroupId = (int)superGroupInfo.SuperGroupId!;

            var nameExists = Identity.Set<Group>()
                .Any(g =>
                g.Id != model.Id &&
                g.SuperGroupId == superGroupInfo.SuperGroupId &&
                g.Name.Trim().ToLower() == model.Name.Trim().ToLower())
                ;

            if (nameExists) this.ThrowException("The branch name already exists");


            group.Name = model.Name.Trim();

            await Identity.UpdateAsync(group).ConfigureAwait(false);

            return new OkResult();
        }
    }
}
