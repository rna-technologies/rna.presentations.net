using rna.Core.Identity.Domain;
using rna.Core.Infrastructure.Logics.Users.Models.EditModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rna.Authorization.Application.Groups.Sas;

public enum GroupTypeSAS
{
    Company, HQ, Branch
}


public class BasicInstitutionalGroupSasModel
{
    public int Id { get; set; }
    //public int AppId { get; set; }
    public int? GroupLocationId { get; set; }
    public int? GroupProfileId { get; set; }
    //public int? SuperGroupId { get; set; } = null;
    public string Description { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;

    public required string GroupProfileName { get; set; } = null!;
    public required string GroupProfileDescription { get; set; } = null!;
    public string? GroupProfilePhoneNumber { get; set; }
    public string? GroupProfilePhoneNumber2 { get; set; }
    public string? GroupProfilePhoneNumber3 { get; set; }
    public string? GroupProfilePhoneNumber4 { get; set; }
    public string? GroupProfileType { get; set; }
    public string? GroupProfileEmail { get; set; }
    public string? GroupProfilePostal { get; set; }
    public string? GroupProfileWebsite { get; set; }



    public string? GroupLocationCountry { get; set; }
    public string? GroupLocationState { get; set; }
    public string? GroupLocationCity { get; set; }
    public string? GroupLocationSuburb { get; set; }
    public string? GroupLocationPlace { get; set; }
}


public class BasicGroupSasModel
{
    public int Id { get; set; }
    //public int AppId { get; set; }
    public string? Description { get; set; }
    public required string Name { get; set; }
    public string? Type { get; set; }
}


public class BasicInstitutionalUserSASModel : BaseSignUpModel, IUserSignUpModel
{

    public string? Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string Gender { get; set; } = "Default";
    public string MiddleName { get; set; } = string.Empty;
    public required string PhoneNumber { get; set; }
    public string? VerifyPassword { get; set; }

    public new required string Email { get; set; }
    public string UserName { get; set; } = string.Empty;
    public new required string? ZipCode { get; set; }
    //public string? Password { get; set; }

    public required string CompanyName { get; set; } = null!;
    public string? CompanyDescription { get; set; } = string.Empty;
    public string? CompanyPhoneNumber { get; set; }
    public string? CompanyPhoneNumber2 { get; set; }
    public string? CompanyPhoneNumber3 { get; set; }
    public string? CompanyPhoneNumber4 { get; set; }
    public string? CompanyType { get; set; }
    public string? CompanyEmail { get; set; }
    public string? CompanyPostal { get; set; }
    public string? CompanyWebsite { get; set; }

    public string? SignUpToken { get; set; }



    public int RegisteredGroupId { get; set; }
    public bool AccountEnabled { get; set; }
    public bool IsTellerable { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsPerson { get; set; } = true;
    public string? FullName { get; set; }
    public bool NextLoginChangePassword { get; set; }
}
