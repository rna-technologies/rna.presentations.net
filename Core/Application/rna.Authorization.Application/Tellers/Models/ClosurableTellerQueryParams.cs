namespace rna.Authorization.Application.Tellers;

public class ClosurableTellerQueryParams
{
    //[FromQuery] public int Id { get; set; }
    [FromQuery] public DateTime Date { get; set; }
    [FromQuery] public int AccountTypeId { get; set; }
    [FromQuery] public int GroupId { get; set; }
    [FromQuery] public int SubscriptionTypeId { get; set; }
}
