using Microsoft.AspNetCore.Mvc;
using System;

namespace rna.Authorization.Application.Models
{
    public class ClosurableTellerQueryParams
    {
        //[FromQuery] public int Id { get; set; }
        [FromQuery] public DateTime Date { get; set; }
        [FromQuery] public int AccountTypeId { get; set; }
        [FromQuery] public int GroupId { get; set; }
        [FromQuery] public int SubscriptionTypeId { get; set; }
    }
}
