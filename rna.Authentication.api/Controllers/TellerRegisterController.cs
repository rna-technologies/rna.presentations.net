using rna.Authorization.Application.Tellers;
using rna.Authorization.Application.TellerRegisters;

namespace rna.Authentication.api.Controllers;

[ApiController]
public class TellerRegisterController : RnaBaseController
{
    public TellerRegisterController() : base(new string[] { "FullName", "PhoneNumber" }) { }

    [HttpGet]
    [AllowAnyDocumentCategory]
    public async Task<IActionResult> GetRegisterableTellers([FromQuery] DateTime? date, [FromQuery] UrlQueryParams param)
    {
        return Ok(await Mediator.Send(new GetRegisterableTellerPage
        {
            Date = date,
            Params = param
        }).ConfigureAwait(false));
    }


    [HttpPost]
    [AllowAnyDocumentCategory]
    public async Task<IActionResult> RegisterTeller(
        [FromQuery] int tellerId,
        [FromQuery] DateTime? date,
        [FromBody] dynamic model,
        [FromQuery] UrlQueryParams param)
    {
        await Mediator.Send(new OpenTellerRegister
        {
            Date = model.date,
            TellerId = model.tellerId,
        }).ConfigureAwait(false);
        return NoContent();
    }
}
