using Microsoft.AspNetCore.Authorization;
using rna.Authorization.Application.Groups.Sas;
using rna.Authorization.Application.TellerRegisters;
using rna.Authorization.Application.Tellers;

namespace rna.Authentication.api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    public class InstitutionalSignUpController : RnaBaseController
    {
        [HttpPost]
        [AllowAnyDocumentCategory]
        public async Task<IActionResult> Create([FromBody] BasicInstitutionalUserSASModel model, UrlQueryParams param)
        {
            await Mediator.Send(new CreateInstitutionalSignUpSAS
            {
                Model = model
            }).ConfigureAwait(false);

            return NoContent();
        }
    }
}
