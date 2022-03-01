//using Authorization.Application.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using rna.Core.Infrastructure.Logics.Users.Verifications.Models;
using rna.Core.Infrastructure.Logics.Users.Verifications.ContactSignInVerification;
using rna.Core.Infrastructure.Logics.Users.SignIns.PasswordSignIn;
using rna.Core.Infrastructure.Logics.Users.SignIns.PasswordSignIn.Models;
using rna.Core.Infrastructure.Logics.Users.Verifications.ContactSignInVerification.Models;
using rna.Core.Infrastructure.Logics.Users.SignIns.CodeSignIn;
using rna.Core.Infrastructure.Logics.Users.SignUps.CodeSignUp;

namespace rna.Authentication.api.Controllers
{

    [AllowAnonymous]
    [Route("{appName}/[controller]/[action]")]
    public class LocalAccountController : BaseApiController
    {
        public LocalAccountController() : base(new string[] { "FullName" }) { }


        //[HttpGet]
        //[AllowAnonymousUser]
        //public async Task<IActionResult> GetBusinessSignUpOptions()
        //{
        //    return Ok(await Mediator.Send(new GetBusinessSignUpOptions()).ConfigureAwait(false));
        //}

        //[HttpGet]
        //[AllowAnonymousUser]
        //public async Task<IActionResult> GetPersonSignUpOptions()
        //{
        //    return Ok(await Mediator.Send(new GetPersonSignUpOptions()).ConfigureAwait(false));
        //}

        [HttpPost]
        [AllowAnonymousUser]
        public async Task<IActionResult> VerifySignInAndSendCode([FromBody] VerificationRequestModel model)
        {
            //return Ok(new { Hello = "", Yes = "" });
            //await ResourceService.Entity<ProductPackType>().Get().ToListAsync().ConfigureAwait(false);

            var hello = await Mediator.Send(new VerifyUserPhoneOrEmail { Model = model }).ConfigureAwait(false);

            return Ok(hello);

        }

        [HttpPost]
        [AllowAnonymousUser]
        public async Task<IActionResult> VerifyUserPhone([FromBody] VerificationRequestModel model)
        {
            return Ok(await Mediator.Send(new VerifyUserPhone { Model = model }).ConfigureAwait(false));
        }

        [HttpPost]
        [AllowAnonymousUser]
        public async Task<IActionResult> VerifyUserEmail([FromBody] VerificationRequestModel model)
        {
            return Ok(await Mediator.Send(new VerifyUserEmail { Model = model }).ConfigureAwait(false));
        }

        //[HttpPost]
        //[AllowAnonymousUser]
        //public async Task<IActionResult> VerifySignUpAndSendCode([FromBody] ClientSignUpModel model)
        //{
        //    return Ok(await Mediator.Send(new VerifyClientSignUp { Model = model }).ConfigureAwait(false));
        //}

        [HttpPost]
        [AllowAnonymousUser]
        public async Task<IActionResult> UserNameOrPhoneOrEmailAndPassword([FromBody] LoginViewModel model, [FromQuery] string appName = null)
        {
            return Ok(await Mediator.Send(new SignInUserNameOrPhoneOrEmailAndPassword
            {
                Model = model,
                AppName = appName
            }).ConfigureAwait(false));
        }

        [HttpPost]
        [AllowAnonymousUser]
        public async Task<IActionResult> UserNameAndPasswordSignIn([FromBody] LoginViewModel model)
        {
            return Ok(await Mediator.Send(new SignInUserNameAndPassword { Model = model }).ConfigureAwait(false));
        }

        [HttpPost]
        [AllowAnonymousUser]
        public async Task<IActionResult> EmailAndPasswordSignIn([FromBody] LoginViewModel model, [FromQuery] string appName = null)
        {
            return Ok(await Mediator.Send(new SignInEmailAndPassword { Model = model }).ConfigureAwait(false));
        }

        [HttpPost]
        [AllowAnonymousUser]
        public async Task<IActionResult> UserCodeSignIn([FromBody] CodeVerificationModel model)
        {
            return Ok(await Mediator.Send(new SignInUserIdKeyAndCode { Model = model }).ConfigureAwait(false));
        }

        [HttpPost]
        [AllowAnonymousUser]
        public async Task<IActionResult> SignUpCodeSignIn([FromBody] CodeVerificationModel model)
        {
            return Ok(await Mediator.Send(new SignUpWithUserIdKeyAndCode
            {
                HasPassword = false,
                ImageDirectory = ImageDirectory,
                IsTellerable = false,
                SetDefaultRole = true,
                Model = model
            }).ConfigureAwait(false));
        }

        [HttpPost]
        [AllowAnonymousUser]
        public async Task<IActionResult> VerifySignUpCode([FromBody] CodeVerificationModel model)
        {
            // This will not send the generated Token to the client
            _ = await Mediator.Send(new SignUpWithUserIdKeyAndCode
            {
                HasPassword = false,
                ImageDirectory = ImageDirectory,
                IsTellerable = false,
                SetDefaultRole = true,
                Model = model
            }).ConfigureAwait(false);

            return Ok();
        }








    }
}
