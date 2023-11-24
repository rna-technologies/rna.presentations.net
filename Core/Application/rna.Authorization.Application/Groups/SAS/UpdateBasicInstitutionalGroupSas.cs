namespace rna.Authorization.Application.Groups.Sas;

public class UpdateBasicInstitutionalGroupSas : IRequest<IActionResult>
{
    public required BasicInstitutionalGroupSasModel Model { get; set; }


    public class UpdateBasicInstitutionalGroupSasHandler(IServiceProvider serviceProvider) : BaseRequestHandler<UpdateBasicInstitutionalGroupSas, IActionResult>(serviceProvider)
    {
        public override async Task<IActionResult> Handle(UpdateBasicInstitutionalGroupSas request, CancellationToken cancellationToken)
        {
            var model = request.Model;

            model.ThrowArgumentExceptionFor(m => m.GroupProfileName?.Trim() is null or "", "Enter Company Name");
            model.ThrowArgumentExceptionFor(m => m.GroupProfilePhoneNumber?.Trim() is null or "", "Enter Company Phone Number");
            model.ThrowArgumentExceptionFor(m => m.GroupProfileEmail?.Trim() is null or "", "Enter Company Email Address");
            model.ThrowArgumentExceptionFor(m => m.GroupProfilePostal?.Trim() is null or "", "Enter Company Postal or GPS Address");
            model.ThrowArgumentExceptionFor(m => m.GroupLocationPlace?.Trim() is null or "", "Enter Company Location or Area");


            model.Name = model.GroupProfileName;


            var superGroup = await Identity.Set<Group>()
                .AsNoTracking()
               .Where(g => g.Id == Scope.GroupId)
               .Select(g => g.SuperGroup)
               .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (superGroup == null)
                model.ThrowException("User's branch does not have a Company or a Head Branch");

            //GroupProfile? profile = Identity.Set<GroupProfile>()
            //    .FirstOrDefault(p => p.Id == superGroup.GroupProfileId);

            if (superGroup.GroupProfileId == null)
                model.ThrowException("Company profile was not found");


            //GroupLocation? location = Identity.Set<GroupLocation>()
            //    .FirstOrDefault(p => p.Id == superGroup.GroupLocationId);

            if (superGroup.GroupLocationId == null)
                model.ThrowException("Company loation was not found");


            GroupProfile groupProfile = new()
            {
                Id = (int)superGroup.GroupProfileId,
                Description = model.GroupProfileDescription ?? string.Empty,
                Email = model.GroupProfileEmail,
                Name = model.GroupProfileName,
                PhoneNumber = model.GroupProfilePhoneNumber!,
                PhoneNumber2 = model.GroupProfilePhoneNumber2,
                PhoneNumber3 = model.GroupProfilePhoneNumber3,
                PhoneNumber4 = model.GroupProfilePhoneNumber4,
                Type = model.GroupProfileType,
                Postal = model.GroupProfilePostal,
                Website = model.GroupProfileWebsite
            };

            GroupLocation groupLocation = new()
            {
                Id = (int)superGroup.GroupLocationId,
                City = model.GroupLocationCity ?? string.Empty,
                Country = model.GroupLocationCountry ?? string.Empty,
                Place = model.GroupLocationPlace ?? string.Empty,
                State = model.GroupLocationState ?? string.Empty,
                Suburb = model.GroupLocationSuburb ?? string.Empty,
            };

            superGroup.Name = model.GroupProfileName;
            superGroup.GroupLocation = groupLocation;
            superGroup.GroupProfile = groupProfile;

            await Identity.UpdateAsync(superGroup);

            return new OkResult();
        }


    }
}