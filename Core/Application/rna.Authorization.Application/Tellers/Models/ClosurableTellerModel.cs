using rna.Core.Identity.Infrastructure.Extensions.RelatedUser.Interfaces;

namespace rna.Authorization.Application.Tellers;

public class ClosurableTellerModel : IUserRelationKey
{
    public int Id { get; set; }
    public int? TellerRegisterId { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string UserId { get; set; }
    public bool IsClosed { get; set; }
    public DateTime? OpenDate { get; set; }
    public DateTime? CloseDate { get; set; }
    //public decimal? CashFloatTotal { get; set; }
    //public decimal? PaymentTotal { get; set; }
    public string Photo { get; set; } = string.Empty;
    //public decimal TransactionPayment { get; set; }
    //public string SubscriptionTypeName { get; set; }
    //public decimal Debt { get; set; }
    //public decimal Credit { get; set; }
}
