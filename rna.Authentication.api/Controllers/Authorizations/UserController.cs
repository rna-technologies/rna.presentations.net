//using Authorization.Application.Logics.Users.Models.EditModels;
using rna.Core.Infrastructure.Logics.Users.Models.EditModels;

namespace rna.Authentication.api.Controllers.Authorizations
{
    [ApiController]
    [AllowParentGroupEdits]
    public class UserController : UserBaseController
    {
        public UserController() : base(new string[] { "fullname", "userName" }) { }

        [HttpGet(Constants.GetUsers)]
        public override Task<IActionResult> GetUsers([FromQuery] UrlQueryParams param)
        {
            return base.GetUsers(param);
        }

        [HttpGet(Constants.GetSelectedUser)]
        public override Task<IActionResult> GetSelectedUser([FromQuery] string userId, [FromQuery] UrlQueryParams param)
        {
            return base.GetSelectedUser(userId, param);
        }

        //[HttpPut(Constants.UpdateBusinessUser)]
        //public override Task<IActionResult> UpdateBusinessUser([FromBody] BusinessSignUpModel model)
        //{
        //    return base.UpdateBusinessUser(model);
        //}

        //[HttpPost(Constants.CreateBusinessUser)]
        //public override Task<IActionResult> CreateBusinessUser([FromBody] BusinessSignUpModel model,
        //    [FromQuery] bool? setDefaultRole,
        //    [FromQuery] bool hasPassword = false,
        //    [FromQuery] bool isTellerable = false)
        //{
        //    return base.CreateBusinessUser(model, setDefaultRole, hasPassword, isTellerable);
        //}

        //[HttpPut(Constants.UpdatePersonUser)]
        //public override Task<IActionResult> UpdatePersonUser([FromBody] PersonSignUpModel model)
        //{
        //    return base.UpdatePersonUser(model);
        //}

        //[HttpPost(Constants.CreatePersonUser)]
        //public override Task<IActionResult> CreatePersonUser([FromBody] PersonSignUpModel model,
        //    [FromQuery] bool? setDefaultRole,
        //    [FromQuery] bool hasPassword = false,
        //    [FromQuery] bool isTellerable = false)
        //{
        //    return base.CreatePersonUser(model, setDefaultRole, hasPassword, isTellerable);
        //}

        [HttpPut(Constants.UpdateDefaultUser)]
        public override Task<IActionResult> UpdateDefaultUser([FromBody] UserSignUpModel model)
        {
            return base.UpdateDefaultUser(model);
        }

        [HttpPut(Constants.UpdateDefaultUserProperties)]
        public override Task<IActionResult> UpdateDefaultUserProperties([FromQuery] string userId, [FromBody] Dictionary<string, object> model)
        {
            return base.UpdateDefaultUserProperties(userId, model);
        }

        [HttpPost(Constants.CreateDefaultUser)]
        public override Task<IActionResult> CreateDefaultUser([FromBody] UserSignUpModel userModel,
            [FromQuery] bool? setDefaultRole,
            [FromQuery] bool? hasPassword)
        {
            return base.CreateDefaultUser(userModel.Set(u => u.RegisteredGroupId == SelectedGroupId.Value), setDefaultRole, hasPassword);
        }

        [HttpPut(Constants.ChangeUserPassword)]
        public override Task<IActionResult> ChangeUserPassword([FromBody] ChangeUserPasswordModel model)
        {
            return base.ChangeUserPassword(model);
        }

        [HttpPut(Constants.ResetUserPassword)]
        public override Task<IActionResult> ResetUserPassword([FromBody] ResetUserPasswordModel model)
        {
            return base.ResetUserPassword(model);
        }
    }
}
