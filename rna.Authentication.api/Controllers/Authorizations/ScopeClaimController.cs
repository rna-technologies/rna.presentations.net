using rna.Core.Infrastructure.Logics.ScopeClaims;
using rna.Core.Infrastructure.Logics.ScopeClaims.Models;

namespace rna.Authentication.api.Controllers.Authorizations
{
    [ApiController]
    [AllowParentGroupEdits]
    [AllowAnyDocumentCategory]
    public class ScopeClaimController : RnaBaseController
    {
        public ScopeClaimController() : base(new string[] { "name" }) { }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] UrlQueryParams param)
        {
            return param?.Id is int id
            ? Ok(await Mediator.Send(new GetScopeClaim { ScopeClaimId = id }).ConfigureAwait(false))
            : Ok(await Mediator.Send(new GetScopeClaimPage { Params = param }).ConfigureAwait(false));
        }

        [HttpGet("user-groups")]
        public async Task<IActionResult> GetUserScopeGroup([FromQuery] string userId, [FromQuery] UrlQueryParams param)
        {
            param.SearchFields = new string[] { "GroupName", "DepartmentName", "AppName" };
            param.OrderByFields = param.SearchFields;
            return Ok(await Mediator.Send(new GetUserScopeGroupPage
            {
                UserId = userId,
                Params = param
            }).ConfigureAwait(false));
        }


        [HttpGet("logged-user-apps")]
        public async Task<IActionResult> GeLoggerScopeApps([FromQuery] UrlQueryParams param)
        {
            return Ok(await Mediator.Send(new GetLoggerScopeAppPage { Params = param }).ConfigureAwait(false));
        }

        [HttpGet("logged-user-groups")]
        public async Task<IActionResult> GeLoggerScopeGroups([FromQuery] int appId, [FromQuery] UrlQueryParams param)
        {
            return Ok(await Mediator.Send(new GetLoggerScopeGroupPage
            {
                AppId = appId,
                Params = param
            }).ConfigureAwait(false));
        }

        [HttpGet("logged-user-account-types")]
        public async Task<IActionResult> GetLoggerAccountTypes([FromQuery] int appId, [FromQuery] int groupId, [FromQuery] UrlQueryParams param)
        {
            return Ok(await Mediator.Send(new GetLoggerScopeDepartmentPage
            {
                AppId = appId,
                GroupId = groupId,
                Params = param
            }).ConfigureAwait(false));
        }

        [HttpGet("logged-user-roles")]
        public async Task<IActionResult> GetLoggerScopeRoles([FromQuery] int appId, [FromQuery] UrlQueryParams param)
        {
            return Ok(await Mediator.Send(new GetLoggerScopeRolePage
            {
                AppId = appId,
                Params = param
            }).ConfigureAwait(false));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ScopeClaimModel model)
        {
            await Mediator.Send(new CreateScopeClaim
            {
                Model = model
            })
            .ConfigureAwait(false);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromQuery] bool? isActive, [FromQuery] int scopeClaimId, [FromQuery] int roleId)
        {
            await Mediator.Send(new UpdateScopeClaim
            {
                ScopeClaimId = scopeClaimId,
                RoleId = roleId,
                IsActive = isActive,
            })
            .ConfigureAwait(false);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await Mediator.Send(new DeleteScopeClaim { Id = id }).ConfigureAwait(false);
            return NoContent();
        }
    }
}
