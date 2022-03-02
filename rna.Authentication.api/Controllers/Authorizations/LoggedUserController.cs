using rna.Core.Infrastructure.Logics.Users.Models.EditModels;

namespace rna.Authentication.api.Controllers.Authorizations
{
    [ApiController]
    [AllowParentGroupEdits]
    public class LoggedUserController : LoggedUserBaseController
    {
        public LoggedUserController() : base(new string[] { "fullname", "userName" }) { }

        [HttpGet(Constants.GetLoggedUser)]
        public override Task<IActionResult> GetLoggedUserProfile(UrlQueryParams param)
        {
            return base.GetLoggedUserProfile(param);
        }

        [HttpPut(Constants.UpdateDefaultUser)]
        public override Task<IActionResult> UpdateDefaultUser([FromBody] UserSignUpModel model)
        {
            return base.UpdateDefaultUser(model);
        }
    }
}
