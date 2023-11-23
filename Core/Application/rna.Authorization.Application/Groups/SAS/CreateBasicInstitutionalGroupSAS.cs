
using rna.Core.Infrastructure.Logics.Users.DefaultUsers.Models;
using rna.Core.Infrastructure.Logics.Users.Models.EditModels;
using rna.Core.Infrastructure.Logics.Users.Validations.Models;
using rna.Core.Infrastructure.Logics.Users.Validations;
using Microsoft.Extensions.Configuration;
using rna.Core.Identity.Domain;
using rna.Core.Infrastructure.Logics.Users.DefaultUsers.Helpers;
using rna.Authorization.Validator;
using rna.Core.Infrastructure.Services.Authorization;
using rna.Authorization.Application.Models;

namespace rna.Authorization.Application.Groups.SAS;

/// <summary>
/// Institutional Group or Branch can only be created by a user if that user is already a part of a branch i.e. HQ
/// This means Super Group, Group Location, and Group Profile, and also a sub Group like HQ must have already been created
/// Note that Company is a Super Group and a HQ is a sub Group of Company
/// </summary>
public class CreateBasicInstitutionalGroupSAS : IRequest<Unit>
{
    public required DefaultGroupSASModel Model { get; set; } = null!;

    public class CreateBasicInstitutionalGroupSASHandler(IServiceProvider serviceProvider) : BaseRequestHandler<CreateBasicInstitutionalGroupSAS, Unit>(serviceProvider)
    {
        public override async Task<Unit> Handle(CreateBasicInstitutionalGroupSAS request, CancellationToken cancellationToken)
        {
            request.Model.ThrowArgumentExceptionFor(r => r.Name is null or "", "Please specify a 'Name'");
            request.Model.ThrowArgumentExceptionFor(r => r.Description is null or "", "Please specify a 'Description'");

            var superGroupInfo = Identity.Set<Group>()
                .Where(g => g.Id == Scope.GroupId)
                .Select(g => new
                {
                    g.SuperGroupId,
                    g.SuperGroup.GroupProfileId,
                    g.SuperGroup.GroupLocationId
                }).FirstOrDefault();

            if (superGroupInfo is null) this.ThrowException("No group or branch was found for the user");


            //GroupProfile profile = new()
            //{
            //    Description = request.Model.GroupProfileDescription,
            //    Email = request.Model.GroupProfileEmail,
            //    Name = request.Model.GroupProfileName!,
            //    Type = request.Model.GroupProfileType,
            //    PhoneNumber = request.Model.GroupProfilePhoneNumber!,
            //    PhoneNumber2 = request.Model.GroupProfilePhoneNumber2,
            //    PhoneNumber3 = request.Model.GroupProfilePhoneNumber3,
            //    Postal = request.Model.GroupProfilePostal,
            //    Website = request.Model.GroupProfileWebsite,
            //};

            //GroupLocation location = new()
            //{
            //    City = string.Empty,
            //    Country = string.Empty,
            //    Place = request.Model.GroupLocationPlace!,
            //    State = string.Empty,
            //    Suburb = string.Empty,
            //};

            //Group company = new()
            //{
            //    AppId = Scope.AppId,
            //    Description = request.Model.Name,
            //    GroupLocation = location,
            //    GroupProfile = profile,
            //    Name = request.Model.Name,
            //    Type = GroupTypeSAS.Company.ToString(),
            //    SuperGroupId = null,
            //};

            Group group = new()
            {
                AppId = Scope.AppId,
                Description = request.Model.Name,
                GroupLocationId = superGroupInfo.GroupLocationId,
                GroupProfileId = superGroupInfo.GroupProfileId,
                Name = request.Model.Name,
                Type = GroupTypeSAS.Branch.ToString(),
                SuperGroupId = superGroupInfo.SuperGroupId
            };

            await Identity.CreateAsync(group).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}



public class CreateBasicInstitutionalUserSAS : IRequest<GroupSASModel>
{
    public required DefaultInstitutionalUserSASModel Model { get; set; }


    public class CreateBasicInstitutionalUserSASHandler(IServiceProvider serviceProvider) : BaseRequestHandler<CreateBasicInstitutionalUserSAS, GroupSASModel>(serviceProvider)
    {
        public override async Task<GroupSASModel> Handle(CreateBasicInstitutionalUserSAS request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            model.UserName = model.Email;

            model.ThrowArgumentExceptionFor(m => m.Password != m.VerifyPassword, "Password does not match");

            model.ThrowArgumentExceptionFor(m => m.SignUpToken?.Trim() is null or "", "Please provide a token");

            var verifyResult = model.SignUpToken!.VerifySerial(model.Email);

            if (verifyResult.Data?.Trim() is null or "")
                model.ThrowException("No permission was found on this Token");


            var splitResult = verifyResult.Data.Split(';');
            var appName = splitResult[0]?.Trim().ToLower();
            var roleName = splitResult[1]?.Trim().ToLower();

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


            await Mediator.Send(new ValidateDefaultUser
            {
                Model = model,
                ValidateType = ValidateType.ProfileCreation
            }, cancellationToken).ConfigureAwait(false);

            var user = model.Map<User>();

            var roleClaim = Identity.GetDefaultRoleClaim(true);


            GroupProfile profile = new()
            {
                Description = string.Empty,
                Email = model.CompanyEmail,
                Name = model.CompanyName,
                PhoneNumber = model.CompanyPhoneNumber!,
                PhoneNumber2 = model.CompanyPhoneNumber2,
                PhoneNumber3 = model.CompanyPhoneNumber3,
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
                AppId = Scope.AppId,
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
                //AppId = app.Id,
                //GroupId = group.Id,
                Group = hQGroup,
                //DepartmentId = department.Id,
                Department = department,
                IsAtive = true,
                RoleClaim = roleClaim
            }.MakeList();

            user.RegistrationInfo = new RegisterationInfo
            {
                Date = Today,
                //AppId = app.Id,
                //GroupId = group.Id,
                Group = hQGroup,
                RegistrarId = Identity.LoggedUserId,
                //UserId = user.Id,
                GroupAssigned = true
            };


            await Identity.CreateWithoutSavingAsync(department);

            var result = await UserManager.CreateAsync(user!, model.Password!).ConfigureAwait(false);


            return new();
        }


        private void CreateScope(User user)
        {
            if (Scope is null)
            {
                var defaultScope = Identity.Configuration
                    .GetSection(nameof(DefaultScope))
                    .Get<DefaultScope>();

                if (defaultScope is null) defaultScope.ThrowException("Please set a default Scope for the App");

                var app = Identity.Set<App>().Get()
                    .FirstOrDefault(a => a.Name.ToLower() == defaultScope.AppName.ToLower());

                if (app is null) app.ThrowException($"The default App '{defaultScope.AppName}' was not found @appsettings");

                var department = Identity.Set<Department>().Get()
                    .FirstOrDefault(a => a.Name.ToLower() == defaultScope.Department.ToLower());

                if (department is null) department.ThrowException($"The default Department '{defaultScope.Department}' was not found @appsettings");

                var group = Identity.Set<Group>().Get()
                    .FirstOrDefault(a => a.Name.ToLower() == defaultScope.GroupName.ToLower());

                if (group is null) group.ThrowException($"The default Group '{defaultScope.GroupName}' was not found @appsettings");

                user.ScopeClaims = new ScopeClaim
                {
                    AppId = app.Id,
                    GroupId = group.Id,
                    DepartmentId = department.Id,
                    IsAtive = true,
                    //RoleClaim = GetRole(request.SetDefaultRole)

                }.MakeList();

                user.RegistrationInfo = new RegisterationInfo
                {
                    Date = Today,
                    AppId = app.Id,
                    GroupId = group.Id,
                    RegistrarId = Identity.LoggedUserId,
                    UserId = user.Id,
                    GroupAssigned = true
                };
            }
        }
    }
}