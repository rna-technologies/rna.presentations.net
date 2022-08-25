


using rna.Core.Identity.Infrastructure.Pageables;
using rna.Core.Infrastructure.CustomAttribute;

namespace rna.Authentication.api.Controllers.Authorizations
{
    [AllowParentGroupEdits]
    public class DocumentCategoryController : BaseApiController
    {

        public DocumentCategoryController() : base(new string[] { "name" }) { }

        [HttpGet]
        public async Task<IActionResult> Get(UrlQueryParams param)
        {
            var queryable = IdentityService.Entity<DocumentCategory>().Get();

            if (param.Id != null)
            {
                var role = queryable.FirstOrDefault(a => a.Id == param.Id);
                return Ok(role);
            }

            var pagable = queryable.ToPageable<DocumentCategoryPageable>(IdentityService.DbContext, param);

            return Ok(pagable);
        }

        [HttpPost]
        [AllowAnyDocumentCategory]
        public async Task<IActionResult> Post([FromBody] DocumentCategory model)
        {
            var DocumentCategory = await IdentityService.CreateAsync(model).ConfigureAwait(false);
            return Created("DocumentCategory", new { DocumentCategory.Id });
        }

        [HttpPut]
        [AllowAnyDocumentCategory]
        public async Task<IActionResult> Put([FromBody] DocumentCategory model)
        {
            var rolePermissionClaim = await IdentityService.UpdateAsync(model).ConfigureAwait(false);
            return Ok(rolePermissionClaim);
        }
    }
}
