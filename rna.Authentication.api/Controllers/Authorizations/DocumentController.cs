using rna.Core.Infrastructure.Services.HttpClients;
using rna.Core.Identity.Infrastructure.Pageables;
using rna.Core.Base.Infrastructure;


namespace rna.Authentication.api.Controllers.Authorizations
{
    [ApiController]
    [AllowParentGroupEdits]
    [AllowAnyDocumentCategory]
    public class DocumentController : BaseApiController
    {

        public DocumentController(ResourceDocumentClient resourceDocumentClient)
            : base(new string[] { "name" })
        {
            ResourceDocumentClient = resourceDocumentClient;
        }

        public ResourceDocumentClient ResourceDocumentClient { get; }

        [HttpGet]
        public async Task<IActionResult> GetAction([FromQuery] UrlQueryParams param)
        {
            var queryable = IdentityService.Entity<Document>().Get()
                .Where(d => d.AppId == Scope.AppId);
            if (param?.Id is { } id)
            {
                var document = await queryable.FirstOrDefaultAsync(d => d.Id == id).ConfigureAwait(false);
                return Ok(document);
            }

            var pagenation = queryable.ToPageable<DocumentPageable>(IdentityService.DbContext, param);

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
            //var dataControllerDocuments = (await ResourceDocumentClient.GetDocumentsAsync<AppDocument>(SelectedApp, "controllers")
            //         .ConfigureAwait(false));
            var dataControllerDocuments = (await FileExtension.GetControllerDocumentsAsync().ConfigureAwait(false)).ToList();

            foreach (var m in FileExtension.GetControllerDocuments())
            {
                if (!dataControllerDocuments.Any(d => d.Name == m.Name))
                    dataControllerDocuments.Add(m);
            }

            var savedDocuments = IdentityService.Entity<Document>().Map<ControllerDocument>().ToList();

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

            var savedDocuments = IdentityService.Entity<Document>().Get()
                .Where(d => d.AppId == Scope.AppId)
                .ToList().Map<List<ControllerDocument>>();

            var notSavedDocuments = dataControllerDocuments.AsQueryable()
                .WhereNotAny(savedDocuments.Select(s => s.Name).ToArray(), d => d.Name)
                .OrderBy(n => n.Name)
                .ToList(); //dataControllerDocuments.Except(savedDocuments, new AppDocumentComparer()).OrderBy(n => n.Name).ToList();

            return Ok(notSavedDocuments);
        }

        //[HttpGet("false-saved/data/list")]
        //public async Task<IActionResult> GetNonExistingDataList([FromQuery] UrlQueryParams param)
        //{
        //    var dataControllerDocuments = await ResourceDocumentClient.GetDocumentsAsync<AppDocument>(SelectedApp)
        //             .ConfigureAwait(false);

        //    foreach (var m in FileExtension.GetControllerDocuments())
        //    {
        //        if (!dataControllerDocuments.Any(d => d.Name == m.Name))
        //            dataControllerDocuments.Add(m.Map<AppDocument>());
        //    }

        //    var savedDocuments = IdentityService.Entity<Document>().Get().ToList().Map<List<AppDocument>>();

        //    var falseSavedDocuments = savedDocuments.Except(dataControllerDocuments).OrderBy(ne => ne.Name).ToList();

        //    return Ok(falseSavedDocuments);
        //}

        [HttpPost]
        public async Task<IActionResult> CreateModel([FromBody] Document model, [FromQuery] UrlQueryParams param)
        {
            try
            {
                model.AppId = Scope.AppId;
                model = await IdentityService.CreateAsync(model).ConfigureAwait(false);

                return Created("Document", new { model.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestModel { error = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateModel([FromBody] Document model, [FromQuery] UrlQueryParams param)
        {
            try
            {
                model.AppId = Scope.AppId;
                await IdentityService.UpdateAsync(model).ConfigureAwait(false);
                return Ok(model.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestModel { error = ex.Message });
            }
        }
    }

}
