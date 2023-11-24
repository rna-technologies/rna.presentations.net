using Microsoft.AspNetCore.Authorization;
using rna.Authorization.Application.Groups.SAS;
using rna.Authorization.Application.TellerRegisters;
using rna.Authorization.Application.Tellers;

namespace rna.Authentication.api.Controllersz
{
    [AllowAnonymous]
    [ApiController]
    public class BasicInstitutionalSignUpController : RnaBaseController
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
