using rna.Core.Infrastructure.Services.HttpClients;
using rna.Core.Identity.Infrastructure.Pageables;
using rna.Core.Base.Infrastructure;
using rna.Exceptions.Extensions;
using rna.Core.Identity.Infrastructure.Models;

namespace rna.Authentication.api.Controllers.Authorizations
{
    [ApiController]
    [AllowParentGroupEdits]
    [AllowAnyDocumentCategory]
    public class DocumentController : BaseApiController
    {
        public DocumentController()
            : base(new string[] { "name" })
        {
            //ResourceDocumentClient = resourceDocumentClient;
        }

        //public ResourceDocumentClient ResourceDocumentClient { get; }

        [HttpGet]
        public async Task<IActionResult> GetAction([FromQuery] int? appId, [FromQuery] UrlQueryParams param)
        {
            if (appId == null) this.ThrowException("Please select an app");

            var queryable = Identity.Set<Document>().Get()
                .Where(d => d.AppId == appId);
            if (param?.Id is { } id)
            {
                var document = await queryable.FirstOrDefaultAsync(d => d.Id == id).ConfigureAwait(false);
                return Ok(document);
            }

            var pagenation = queryable.ToPageable<DocumentPageable>(Identity.DbContext, param);

            return Ok(pagenation);
        }

        [HttpGet("controllers")]
        [AllowAnyDocumentCategory]
        //[AllowAnonymous]
        public async Task<IActionResult> GetAction([FromQuery] string name)
        {
            if (name != null)
            {
                return Ok(FileExtension.GetControllerDocuments().FirstOrDefault(d => d.Name == name));
            }

            var result = (await FileExtension.GetControllerDocumentsAsync().ConfigureAwait(false)).ToList();
            return Ok(result);
        }

        private string SelectedApp => "Inventory";

        [HttpGet("fields/data/list")]
        public async Task<IActionResult> GetFieldsDataList([FromQuery] UrlQueryParams param)
        {
            var dataControllerDocuments = (await FileExtension.GetControllerDocumentsAsync().ConfigureAwait(false)).ToList();

            foreach (var m in FileExtension.GetControllerDocuments())
            {
                if (!dataControllerDocuments.Any(d => d.Name == m.Name))
                    dataControllerDocuments.Add(m);
            }

            var savedDocuments = Identity.Set<Document>().Map<ControllerDocument>().ToList();

            var notSavedDocuments = dataControllerDocuments.AsQueryable()
                .WhereNotAny(savedDocuments.Select(s => s.Name).ToArray(), d => d.Name)
                .OrderBy(n => n.Name)
                .ToList(); //dataControllerDocuments.Except(savedDocuments).OrderBy(n => n.Name);

            var falseSavedDocuments = savedDocuments.AsQueryable()
                .WhereNotAny(dataControllerDocuments.Select(d => d.Name).ToArray(), s => s.Name)
                .OrderBy(ne => ne.Name); //savedDocuments.Except(dataControllerDocuments).OrderBy(ne => ne.Name);

            return Ok(new { SavedDocuments = savedDocuments, NotSavedDocuments = notSavedDocuments, FalseSavedDocuments = falseSavedDocuments });
        }



        [HttpGet("unsaved")]
        public async Task<IActionResult> GetNotSavedDataList([FromQuery] UrlQueryParams param)
        {
            //var dataControllerDocuments = (await ResourceDocumentClient.GetDocumentsAsync<AppDocument>(SelectedApp)
            //         .ConfigureAwait(false));

            var dataControllerDocuments = (await FileExtension.GetControllerDocumentsAsync().ConfigureAwait(false)).ToList();

            foreach (var m in FileExtension.GetControllerDocuments())
            {
                if (!dataControllerDocuments.Any(d => d.Name == m.Name))
                    dataControllerDocuments.Add(m);
            }

            var savedDocuments = Identity.Set<Document>()
                .Where(d => d.AppId == Scope.AppId)
                .Map<ControllerDocument>()
                .ToList();

            var notSavedDocuments = dataControllerDocuments.AsQueryable()
                .WhereNotAny(savedDocuments.Select(s => s.Name).ToArray(), d => d.Name)
                .OrderBy(n => n.Name)
                .ToList(); //dataControllerDocuments.Except(savedDocuments, new AppDocumentComparer()).OrderBy(n => n.Name).ToList();

            return Ok(notSavedDocuments);
        }

        [HttpPost]
        public async Task<IActionResult> CreateModel([FromBody] DocumentModel model, [FromQuery] UrlQueryParams param)
        {
            model.ThrowArgumentExceptionFor(m => m.AppId == null || m.AppId == 0);
            await Identity.CreateAsync(model.Map<Document>())
                .ConfigureAwait(false);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateModel([FromBody] DocumentModel model, [FromQuery] UrlQueryParams param)
        {
            model.ThrowArgumentExceptionFor(m => m.AppId == null || m.AppId == 0);
            var doc = model.Map<Document>();
            await Identity.UpdateAsync(doc).ConfigureAwait(false);
            return Ok();
        }
    }

}
