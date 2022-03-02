using rna.Core.Infrastructure.Logics.ScopeClaims;
using rna.Core.Infrastructure.Logics.ScopeClaims.Models;

namespace rna.Authentication.api.Controllers.Authorizations
{
    [ApiController]
    [AllowParentGroupEdits]
    public class UserRoleController : BaseApiController
    {
        public UserRoleController() { }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] UrlQueryParams param)
        {

            return param?.Id is int id
            ? Ok(await Mediator.Send(new GetScopeClaim { ScopeClaimId = id }).ConfigureAwait(false))
            : Ok(await Mediator.Send(new GetScopeClaimPage { Params = param }).ConfigureAwait(false));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromQuery] string userId, [FromQuery] int groupId)
        {
            await Mediator.Send(new CreateScopeClaim
            {
                Model = new ScopeClaimModel { UserId = userId, GroupId = groupId }
            })
            .ConfigureAwait(false);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromQuery] int scopeClaimId, [FromQuery] int groupId)
        {
            await Mediator.Send(new UpdateScopeClaim
            {
                ScopeClaimId = scopeClaimId,
                RoleId = groupId
            })
            .ConfigureAwait(false);
            return NoContent();
        }
    }
}
