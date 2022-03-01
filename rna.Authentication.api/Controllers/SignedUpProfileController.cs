//using Authorization.Application.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using rna.Core.Infrastructure.Logics.Users.Models.EditModels;

namespace rna.Authentication.api.Controllers
{
    [AllowAnyDocumentCategory]
    public class SignedUpProfileController : LoggedUserBaseController
    {
        public SignedUpProfileController() : base(new string[] { "GenericName" }) { }

        [HttpGet(Constants.GetLoggedUser)]
        public override Task<IActionResult> GetLoggedUserProfile(UrlQueryParams param)
        {
            return base.GetLoggedUserProfile(param);
        }

        //[HttpPut(Constants.UpdateBusiness)]
        //public override Task<IActionResult> UpdateBusiness([FromBody] BusinessSignUpModel model)
        //{
        //    return base.UpdateBusiness(model);
        //}

        //[HttpPut(Constants.UpdatePerson)]
        //public override Task<IActionResult> UpdatePerson([FromBody] PersonSignUpModel model)
        //{
        //    return base.UpdatePerson(model);
        //}

        [HttpPut(Constants.UpdateDefaultUser)]
        public override Task<IActionResult> UpdateDefaultUser([FromBody] UserSignUpModel model)
        {
            return base.UpdateDefaultUser(model);
        }

    }
}
