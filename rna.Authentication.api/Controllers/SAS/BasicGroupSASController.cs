using Microsoft.AspNetCore.Authorization;
using rna.Authorization.Application.Groups.SAS;
using rna.Authorization.Application.TellerRegisters;
using rna.Authorization.Application.Tellers;

namespace rna.Authentication.api.Controllersx
{
    [ApiController]
    public class BasicGroupSASController : RnaBaseController
    {
        [HttpGet]
        [AllowAnyDocumentCategory]
        public async Task<IActionResult> Get([FromQuery] UrlQueryParams param)
        {
            return await Mediator.Send(new GetBasicInstitutionGroupSAS()).ConfigureAwait(false);
        }
    }
}
