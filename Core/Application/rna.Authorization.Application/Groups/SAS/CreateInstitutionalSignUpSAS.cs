using rna.Core.Infrastructure.Logics.Users.Validations.Models;
using rna.Core.Infrastructure.Logics.Users.Validations;
using rna.Core.Infrastructure.Services.Authorization;

namespace rna.Authorization.Application.Groups.Sas;

public class CreateInstitutionalSignUpSAS : IRequest<Unit>
{
    public required BasicInstitutionalUserSASModel Model { get; set; }


    public class CreateInstitutionalSignUpSASHandler(IServiceProvider serviceProvider) : BaseRequestHandler<CreateInstitutionalSignUpSAS, Unit>(serviceProvider)
    {
        public override async Task<Unit> Handle(CreateInstitutionalSignUpSAS request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            model.Id = Guid.NewGuid().ToString();
            model.UserName = model.Email;

            model.ThrowArgumentExceptionFor(m => m.Password?.Trim() is null or "" || m.Password != m.VerifyPassword, "Please provide a password");

            model.ThrowArgumentExceptionFor(m => m.Password != m.VerifyPassword, "Password does not match");

            model.ThrowArgumentExceptionFor(m => m.SignUpToken?.Trim() is null or "", "Please provide a token");

            var verifyResult = model.SignUpToken!.VerifySerial(model.Email);

            if (verifyResult.Data?.Trim() is null or "")
                model.ThrowException("No permission was found on this Token");


            var splitData = verifyResult.Data.Split(';');

            if (splitData.Length < 2)
                model.ThrowException("The Token is incorrect");

            string? appName = string.Empty, roleName = string.Empty;
            if (splitData.Length > 0)
            {
                appName = splitData[0]?.Trim().ToLower();
                roleName = splitData[1]?.Trim().ToLower();

                if (appName is null or "" || roleName is null or "")
                    model.ThrowException("The Token is incorrect");
            }

            await Mediator.Send(new ValidateDefaultUser
            {
                Model = model,
                ValidateType = ValidateType.ProfileCreation
            }, cancellationToken).ConfigureAwait(false);


            var app = Identity.Set<App>()
                .Where(a => a.Name.Trim().ToLower() == appName)
                .Select(a => new { a.Id, a.Name })
                .FirstOrDefault();

            if (app == null)
                model.ThrowException("App permission was not found on this Token");


            var role = Identity.Set<Role>()
                .Where(r => r.Name.Trim().ToLower() == roleName && r.AppId == app.Id)
                .Select(a => new { a.Id, a.Name })
                .FirstOrDefault();

            if (role == null)
                model.ThrowException("Role permission was not found on this Token");


            RoleClaim roleClaim = new()
            {
                RoleId = role.Id,
                IsActive = true
            };

            var user = model.Map<User>();

            if (user is null) model.ThrowException("User details cannot be empty");

            GroupProfile profile = new()
            {
                Description = model.CompanyDescription ?? string.Empty,
                Email = model.CompanyEmail,
                Name = model.CompanyName,
                PhoneNumber = model.CompanyPhoneNumber!,
                PhoneNumber2 = model.CompanyPhoneNumber2,
                PhoneNumber3 = model.CompanyPhoneNumber3,
                PhoneNumber4 = model.CompanyPhoneNumber4,
                Type = model.CompanyType,
                Postal = model.CompanyPostal,
                Website = model.CompanyWebsite,
            };

            GroupLocation location = new()
            {
                City = string.Empty,
                Country = string.Empty,
                Place = string.Empty,
                State = string.Empty,
                Suburb = string.Empty,
            };

            Group company = new()
            {
                AppId = app.Id,
                Description = model.CompanyName,
                GroupLocation = location,
                GroupProfile = profile,
                Name = model.CompanyName,
                Type = GroupTypeSAS.Company.ToString(),
                SuperGroupId = null,
            };

            Group hQGroup = new()
            {
                AppId = app.Id,
                Description = string.Empty,
                GroupLocation = location,
                GroupProfile = profile,
                Name = "HQ",
                Type = GroupTypeSAS.HQ.ToString(),
                SuperGroup = company
            };

            Department department = new()
            {
                Description = string.Empty,
                Group = hQGroup,
                Name = "Default"
            };


            user.ScopeClaims = new ScopeClaim
            {
                AppId = app.Id,
                Group = hQGroup,
                Department = department,
                IsAtive = true,
                RoleClaim = roleClaim
            }.MakeList();

            user.RegistrationInfo = new RegisterationInfo
            {
                Date = Today,
                AppId = app.Id,
                //GroupId = group.Id,
                Group = hQGroup,
                RegistrarId = Identity.LoggedUserId,
                //UserId = user.Id,
                GroupAssigned = true
            };


            var result = await UserManager.CreateAsync(user!, model.Password!).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}
