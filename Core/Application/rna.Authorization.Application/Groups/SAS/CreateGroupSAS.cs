
namespace rna.Authorization.Application.Groups.SAS;

public class CreateGroupSAS : IRequest<Unit>
{
    public required GroupSASModel Model { get; set; } = null!;
}
public class CreateGroupHandler : BaseRequestHandler<CreateGroupSAS, Unit>
{
    public CreateGroupHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
    public override async Task<Unit> Handle(CreateGroupSAS request, CancellationToken cancellationToken)
    {
        request.Model.ThrowArgumentExceptionFor(r => r.Name is null or "", "Please specify a 'Name'");
        request.Model.ThrowArgumentExceptionFor(r => r.Description is null or "", "Please specify a 'Description'");

        var group = new Group
        {
            AppId = Scope.AppId,
            Description = request.Model.Description,
            GroupLocationId = request.Model.GroupLocationId,
            GroupProfileId = request.Model.GroupProfileId,
            Name = request.Model.Name,
            Type = request.Model.Type,
            SuperGroupId = request.Model.SuperGroupId,
        };

        await Identity.CreateAsync(group).ConfigureAwait(false);

        return Unit.Value;
    }
}

