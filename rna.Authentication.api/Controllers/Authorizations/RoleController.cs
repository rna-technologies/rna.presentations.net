using rna.Core.Identity.Infrastructure.Models;
using rna.Core.Infrastructure.Logics.Roles;
using rna.Core.Infrastructure.Logics.Roles.Models;
using rna.Exceptions.Extensions;

namespace rna.Authentication.api.Controllers.Authorizations;

[AllowParentGroupEdits]
public class RoleController : BaseApiController
{

    public RoleController() : base(new string[] { "name" }) { }

    [HttpGet]
    [AllowAnyDocumentCategory]
    public async Task<IActionResult> Get([FromQuery] int? appId, UrlQueryParams param)
    {
        appId = appId is int id and > 0 ? appId : Scope.AppId;
        var query = Identity.Entity<Role>().Get()
            .AsNoTracking()
            .Where(r => r.AppId == appId)
            .Select(r => new RoleModel
            {
                AppId = r.AppId,
                Description = r.Description,
                Id = r.Id,
                Name = r.Name
            });

        if (param?.Id != null)
        {
            var role = await query.FirstOrDefaultAsync(r => r.Id == param.Id)
                .ConfigureAwait(false);
            return Ok(role);
        }

        var pagable = query.ToPageable(Identity.DbContext, param);

        return Ok(pagable);
    }

    [HttpGet("document")]
    [AllowAnyDocumentCategory]
    public Task<IActionResult> GetDocuments([FromQuery] int? appId, [FromQuery] int roleId, [FromQuery] UrlQueryParams param)
    {
        var roleDocumentIds = Identity.Entity<DocumentClaim>().Get()
            .Where(d => d.RoleId == roleId && d.DocumentId != null)
            .Select(d => d.DocumentId.ToString()).ToArray();

        appId = appId ?? Scope.AppId;
        var pagable = Identity.Entity<Document>().Get()
            .Where(d => d.AppId == appId)
            .WhereNotAny(roleDocumentIds, d => d.Id)
            .Select(d => new DocumentModel
            {
                Id = d.Id,
                AppId = d.AppId,
                Description = d.Description,
                IsGrantDocument = d.IsGrantDocument,
                Name = d.Name
            }).ToPageable(Identity.DbContext, param);

        return Task.FromResult(Ok(pagable) as IActionResult);
    }


    [HttpGet("document-claim")]
    [AllowAnyDocumentCategory]
    public Task<IActionResult> GetRoleDocumentClaims([FromQuery] int roleId, UrlQueryParams param)
    {
        var values = new string[] { "documentName" };
        param.Set(p => p.SearchFields == values).Set(p => p.OrderByFields == values);

        var hello = Identity.Entity<DocumentClaim>().Get()
             .Where(d => d.RoleId == roleId)
             .AsNoTracking()
             .Select(d => new DocumentClaimEditModel
             {
                 Id = d.Id,
                 DocumentId = d.Document.Id,
                 RoleId = d.RoleId,
                 RoleClaimId = d.RoleClaimId,
                 CustomGrantClaimId = d.CustomGrantClaimId,
                 DocumentName = d.Document.Name,
                 IsActive = d.IsActive
             }).ToPageable(Identity.DbContext, param);

        var documentClaimIds = hello.Data.Select(d => d.Id).ToList();

        var categoryClaims = Identity.Entity<CategoryClaim>().Get()
            .FindAny(documentClaimIds, c => c.DocumentClaimId)
            .Select(c => new CategoryClaimEditModel
            {
                DocumentClaimId = c.DocumentClaimId,
                CategoryName = c.DocumentCategory.Name,
                DocumentCategoryId = c.DocumentCategoryId,
                Id = c.Id,
                IsActive = c.IsActive,
                CategoryTypeClaimEditModels = c.CategoryTypeClaims.Select(t => new CategoryTypeClaimEditModel
                {
                    Id = t.Id,
                    CategoryClaimId = t.CategoryClaimId,
                    Create = t.Create,
                    Delete = t.Delete,
                    Read = t.Read,
                    Update = t.Update
                }).ToList()
            }).ToList();



        foreach (var documentClaim in hello.Data)
        {
            documentClaim.CategoryClaimEditModels = categoryClaims.Where(c => c.DocumentClaimId == documentClaim.Id).ToList();
        }

        return Task.FromResult(Ok(hello) as IActionResult);
    }



    [HttpPost("document-claim")]
    [AllowAnyDocumentCategory]
    public async Task<IActionResult> CreateClaims([FromQuery] int roleId, [FromBody] DocumentModel model)
    {
        if (roleId == 0) this.ThrowException("The selected Role could not be found");

        if (string.IsNullOrEmpty(model.Name?.Trim())) this.ThrowException("There is no name for the selected document");


        var documentCategory = Identity.Entity<DocumentCategory>()
                     .Where(dc => dc.DocumentId == model.Id && dc.Name.Trim().ToLower() == "Any".ToLower())
                     .ToList()?.FirstOrDefault();

        //var documentCategoryType = IdentityService.Entity<DocumentCategoryType>().Get()
        //     .Where(dc => dc.DocumentId == model.Id && dc.Name.Trim().ToLower() == "Any".ToLower())
        //     .ToList()?.FirstOrDefault();

        if (documentCategory is null)
            documentCategory = Identity
                .CreateWithoutSaving(new DocumentCategory
                {
                    Description = "Any",
                    DocumentId = model.Id,
                    Id = 0,
                    Name = "Any"
                });

        var documentClaim = new DocumentClaim
        {
            DocumentId = model.Id,
            IsActive = true,
            Id = 0,
            RoleId = roleId,
            CustomGrantClaim = null,
            RoleClaim = null,
            CategoryClaims = new CategoryClaim
            {
                DocumentCategory = documentCategory,
                DocumentCategoryId = documentCategory.Id,
                Id = 0,
                IsActive = true,
                CategoryTypeClaims = new CategoryTypeClaim
                {
                    Create = false,
                    Delete = false,
                    Id = 0,
                    Read = false,
                    Update = false,
                }.MakeList(),
            }.MakeList()
        };


        Identity.CreateWithoutSaving(documentClaim);

        var saveed = (await Identity.SaveChangesAsync().ConfigureAwait(false)) > 0;

        if (saveed) Identity.SetClearUserDocmentCache(model.Name);

        return Ok(model);
    }




    [HttpPut("document-claim")]
    [AllowAnyDocumentCategory]
    public Task<IActionResult> UpdateClaims([FromBody] DocumentClaimEditModel model)
    {
        var documentClaim = model.Map<DocumentClaim>();



        var categoryClaims = model.CategoryClaimEditModels.Map<List<CategoryClaim>>();


        foreach (var categoryClaim in categoryClaims)
        {
            if (categoryClaim.DocumentClaimId == 0) HttpContext.ThrowException("Please select a document Claim for the Category Claim");

            var categoryTypeClaims = model.CategoryClaimEditModels
                .First(c => c.Id == categoryClaim.Id)
                .CategoryTypeClaimEditModels
                .Map<List<CategoryTypeClaim>>();

            if (categoryClaim.DocumentCategoryId == 0)
            {
                var documentCategoryId = Identity.Entity<DocumentCategory>().Get()
                       .Where(c => c.DocumentId == documentClaim.DocumentId && c.Name.ToLower() == "any")
                       .FirstOrDefault()?
                       .Id;

                if (documentCategoryId is null || documentCategoryId == 0)
                    HttpContext.ThrowException("Document Category 'Any' does not exist. Please create one");

                categoryClaim.DocumentCategoryId = documentCategoryId.Value;
            }

            categoryClaim.CategoryTypeClaims = categoryTypeClaims;
        }

        documentClaim.CategoryClaims = categoryClaims;

        var _ = documentClaim.Id > 0 ?
             Identity.DetachAllEntities().UpdateWithoutSaving(documentClaim) :
             Identity.DetachAllEntities().CreateWithoutSaving(documentClaim);


        foreach (var categoryClaim in documentClaim.CategoryClaims)
        {
            //if (categoryClaim.Id == 0)
            //{
            //    CreateCategoryClaim(documentClaim, categoryClaim);

            //}

            var cc = categoryClaim.Id > 0 ?
                Identity.UpdateWithoutSaving(categoryClaim) :
                Identity.CreateWithoutSaving(categoryClaim);

            foreach (var typeClaim in categoryClaim.CategoryTypeClaims)
            {
                var ct = typeClaim.Id > 0 ?
                Identity.UpdateWithoutSaving(typeClaim) :
                Identity.CreateWithoutSaving(typeClaim);
            }
        }

        documentClaim.CategoryClaims = categoryClaims;

        var saved = Identity.SaveChanges() > 0;

        if (saved)
        {
            var documentName = Identity.Entity<Document>().Get()
                 .Where(d => d.Id == model.DocumentId)
                 .Select(d => d.Name)
                 .FirstOrDefault();

            Identity.SetClearUserDocmentCache(documentName);
        }

        return Task.FromResult(Ok(model) as IActionResult);
    }


    [HttpPost]
    [AllowAnyDocumentCategory]
    public async Task<IActionResult> Post([FromBody] RoleModel model)
    {

        await Mediator.Send(new CreateRole { Model = model })
        .ConfigureAwait(false);
        return NoContent();

    }

    [HttpPut]
    [AllowAnyDocumentCategory]
    public async Task<IActionResult> Put([FromBody] RoleModel model)
    {
        await Mediator.Send(new UpdateRole { Model = model })
        .ConfigureAwait(false);
        return NoContent();
    }
}
