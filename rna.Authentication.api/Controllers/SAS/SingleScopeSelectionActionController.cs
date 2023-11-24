using rna.Core.Base.Infrastructure.Model.ScopeModels;
using rna.Core.Infrastructure.Logics.GroupAccess;
using rna.Exceptions.Extensions;

namespace rna.Authentication.api.Controllers
{
    public class SingleScopeSelectionActionController : RnaBaseController
    {
        public SingleScopeSelectionActionController() : base(new string[] { "name", "Description" }) { }

        [HttpPost]
        public async Task<IActionResult> UpdateSelectedScope([FromBody] ScopeBaseModel model)
        {
            var appName = Identity.SelectedAppName.ToLower();

            var app = Identity.Set<App>()
                .Where(a => a.Name.ToLower() == appName)
                .Select(a => new { a.Id, a.Name }).FirstOrDefault();

            if (app == null)
                this.ThrowException($"The selected app '{appName.ToSentenceCase()}' is unavailable");

            var userId = Identity.LoggedUserId;
            var appId = app.Id;

            var claim = Identity.Set<ScopeClaim>()
                .Where(c =>
                c.UserId == userId &&
                c.AppId == appId
                ).FirstOrDefault();

            if (claim == null) Identity.ThrowException($"You do not have a Scope permission on this app : '{app.Name}'");

            await Mediator.Send(new UpdateAuthorizedCachedScope
            {
                Model = new()
                {
                    AppId = claim.AppId,
                    DepartmentId = claim.DepartmentId,
                    GroupId = claim.GroupId,
                }
            }).ConfigureAwait(false);

            return Ok();
        }


    }
}