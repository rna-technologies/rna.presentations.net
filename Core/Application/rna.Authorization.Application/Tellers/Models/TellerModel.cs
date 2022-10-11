using rna.Core.Identity.Infrastructure.Extensions.RelatedUser.Interfaces;

namespace rna.Authorization.Application.Tellers;

public class TellerModel : IUserRelationKey
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string TellerType { get; set; }
    public DateTime RegisteredDate { get; set; }
    public bool IsDeleted { get; set; }
}