using rna.Authorization.Application.Tellers;

namespace rna.Authentication.api.Controllers
{
    [ApiController]
    public class TellerController : RnaBaseController
    {

        public TellerController() : base(new string[] { "FullName", "PhoneNumber" }) { }

        [HttpGet("users")]
        [AllowAnyDocumentCategory]
        public async Task<IActionResult> GetUsersAction([FromQuery] string userId, [FromQuery] int? appId, [FromQuery] UrlQueryParams param)
        {
            return userId != null
            ? Ok(await Mediator.Send(new GetTellerableUser { UserId = userId }).ConfigureAwait(false))
            : Ok(await Mediator.Send(new GetTellerableUserPage { Params = param }).ConfigureAwait(false));
        }

        [HttpPost]
        [AllowAnyDocumentCategory]
        public async Task<IActionResult> CreateAction([FromQuery] string userId, [FromQuery] int? appId)
        {
            await Mediator.Send(new CreateTeller { UserId = userId, AppId = appId }).ConfigureAwait(false);
            return NoContent();
        }

        [HttpDelete]
        [AllowAnyDocumentCategory]
        public async Task<IActionResult> DeleteAction([FromQuery] string userId, [FromQuery] int? appId)
        {
            await Mediator.Send(new DeleteTeller { UserId = userId, AppId = appId }).ConfigureAwait(false);
            return NoContent();
        }
    }
}
