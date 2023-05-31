using rna.Core.Identity.Infrastructure.Pageables;

namespace rna.Authentication.api.Controllers.Authorizations;

[AllowParentGroupEdits]
public class DocumentCategoryController : RnaBaseController
{

    public DocumentCategoryController() : base(new string[] { "name" }) { }

    [HttpGet]
    public async Task<IActionResult> Get(UrlQueryParams param)
    {
        var queryable = Identity.Entity<DocumentCategory>().Get();

        if (param.Id != null)
        {
            var role = queryable.FirstOrDefault(a => a.Id == param.Id);
            return Ok(role);
        }

        var pagable = queryable.ToPageable<DocumentCategoryPageable>(Identity.DbContext(), param);

        return Ok(pagable);
    }

    [HttpPost]
    [AllowAnyDocumentCategory]
    public async Task<IActionResult> Post([FromBody] DocumentCategory model)
    {
        var DocumentCategory = await Identity.CreateAsync(model).ConfigureAwait(false);
        return Created("DocumentCategory", new { DocumentCategory.Id });
    }

    [HttpPut]
    [AllowAnyDocumentCategory]
    public async Task<IActionResult> Put([FromBody] DocumentCategory model)
    {
        var rolePermissionClaim = await Identity.UpdateAsync(model).ConfigureAwait(false);
        return Ok(rolePermissionClaim);
    }
}
