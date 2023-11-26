using Microsoft.AspNetCore.Authorization;
using rna.Authorization.Application.Groups.Sas;
using rna.Authorization.Application.TellerRegisters;
using rna.Authorization.Application.Tellers;

namespace rna.Authentication.api.Controllers
{
    [ApiController]
    [AllowAnyDocumentCategory]
    public class BranchSasController : RnaBaseController
    {
        [HttpGet("institution")]
        public async Task<IActionResult> GetInstitution([FromQuery] UrlQueryParams param)
        {
            return await Mediator.Send(new GetBasicInstitutionGroupSas()).ConfigureAwait(false);
        }


        [HttpGet]
        public async Task<IActionResult> GetInstitutionBranches([FromQuery] UrlQueryParams param)
        {
            return await Mediator.Send(new GetBasicGroupsSas()).ConfigureAwait(false);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateBranch(
            [FromBody] BasicGroupSasModel model,
            [FromQuery] UrlQueryParams param)
        {
            return await Mediator.Send(new UpdateBasicGroupSas
            {
                Model = model
            }).ConfigureAwait(false);
        }


        [HttpPut("institution")]
        public async Task<IActionResult> UpdateInstitution(
           [FromBody] BasicInstitutionalGroupSasModel model,
           [FromQuery] UrlQueryParams param)
        {
            return await Mediator.Send(new UpdateBasicInstitutionalGroupSas
            {
                Model = model
            }).ConfigureAwait(false);
        }


        [HttpPost]
        public async Task<IActionResult> CreateBranch(
            [FromBody] BasicGroupSasModel model,
            [FromQuery] UrlQueryParams param)
        {
            return await Mediator.Send(new CreateBasicGroupSas
            {
                Model = model
            }).ConfigureAwait(false);
        }

    }
}
