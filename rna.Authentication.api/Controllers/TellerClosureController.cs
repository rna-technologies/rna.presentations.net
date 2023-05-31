using rna.Authorization.Application.TellerRegisters;
using rna.Authorization.Application.Tellers;

namespace rna.Authentication.api.Controllers
{
    [ApiController]
    public class TellerClosureController : RnaBaseController
    {
        public TellerClosureController() : base(new string[] { "FullName", "PhoneNumber" }) { }

        [HttpGet("closurable-by-register")]
        [AllowAnyDocumentCategory]
        public async Task<IActionResult> GetClosurableTellers([FromQuery] UrlQueryParams param)
        {
            return Ok(await Mediator.Send(new GetClosurableTellerByRegisterPage
            {
                Params = param
            }).ConfigureAwait(false));
        }


        [HttpPost]
        [AllowAnyDocumentCategory]
        public async Task<IActionResult> CloseRegisterAction([FromQuery] int tellerId, [FromQuery] decimal moneyAtHand, UrlQueryParams param)
        {
            await Mediator.Send(new CloseTellerRegister
            {
                TellerId = tellerId,
            }).ConfigureAwait(false);

            return NoContent();
        }
    }
}
