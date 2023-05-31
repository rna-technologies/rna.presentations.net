using rna.Core.Base.Infrastructure.Model.ScopeModels;
using rna.Core.Infrastructure.Logics.GroupAccess;

namespace rna.Authentication.api.Controllers
{
    public class ScopeSelectionActionController : RnaBaseController
    {
        public ScopeSelectionActionController() : base(new string[] { "name", "Description" }) { }


        [HttpGet("selectedGroup")]
        public async Task<IActionResult> GetAuthorizedCachedGroup()
        {
            return Ok(await Mediator.Send(new GetAuthorizedCachedScope { }).ConfigureAwait(false));
        }

        [HttpGet("apps")]
        public async Task<IActionResult> GetUserApps([FromQuery] UrlQueryParams param)
        {
            return Ok(await Mediator.Send(new GetAuthorizedAppPage
            {
                Params = param
            }).ConfigureAwait(false));
        }


        [HttpGet("groups")]
        public async Task<IActionResult> GetUserGroups([FromQuery] int appId, [FromQuery] int accountTypeId, [FromQuery] UrlQueryParams param)
        {
            return Ok(await Mediator.Send(new GetAuthorizedGroupPage
            {
                AppId = appId,
                DepartmentId = accountTypeId,
                Params = param
            }).ConfigureAwait(false));
        }

        [HttpGet("departments")]
        public async Task<IActionResult> GetUserAccountTypes([FromQuery] int appId, [FromQuery] UrlQueryParams param)
        {
            return Ok(await Mediator.Send(new GetAuthorizedDepartmentPage
            {
                AppId = appId,
                Params = param
            }).ConfigureAwait(false));
        }

        [HttpGet("groups-vNext")]
        public async Task<IActionResult> GetUserGroupsVNext([FromQuery] int appId, [FromQuery] UrlQueryParams param)
        {
            return Ok(await Mediator.Send(new GetAuthorizedGroupPageVNext
            {
                AppId = appId,
                Params = param
            }).ConfigureAwait(false));
        }

        [HttpGet("departments-vNext")]
        public async Task<IActionResult> GetUserAccountTypesvNext([FromQuery] int appId, [FromQuery] int groupId, [FromQuery] UrlQueryParams param)
        {
            return Ok(await Mediator.Send(new GetAuthorizedDepartmentPageVNext
            {
                AppId = appId,
                GroupId = groupId,
                Params = param
            }).ConfigureAwait(false));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSelectedScope([FromBody] ScopeBaseModel model)
        {
            await Mediator.Send(new UpdateAuthorizedCachedScope { Model = model }).ConfigureAwait(false);
            return NoContent();
        }


    }
}