using rna.Core.Identity.Domain.Base.Interfaces;

namespace rna.Authorization.Application.Tellers;

public class RegisterableTellerModel : IUserRelationKey
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime RegisteredDate { get; set; }
    public bool IsRegistered { get; set; }
    public string TellerType { get; set; }
    public string UserId { get; set; }
    public bool IsClosed { get; set; }
}