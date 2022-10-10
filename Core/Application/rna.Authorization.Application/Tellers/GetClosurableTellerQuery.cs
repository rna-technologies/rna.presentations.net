using MediatR;
using System.Linq;
using System;
using rna.Authorization.Application.Models;

namespace rna.Authorization.Application
{
    public class GetClosurableTellerQuery : IRequest<IQueryable<ClosurableTellerModel>>
    {
        public DateTime Date { get; set; }
        public required UrlQueryParams Params { get; set; }
        public int GroupId { get; set; }
        public int AccountTypeId { get; set; }
        public int SubscriptionTypeId { get; set; }
    }

    public class GetClosurableTellerQueryHandler : BaseRequestHandler<GetClosurableTellerQuery, IQueryable<ClosurableTellerModel>>
    {
        public GetClosurableTellerQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }
        public override Task<IQueryable<ClosurableTellerModel>> Handle(GetClosurableTellerQuery request, CancellationToken cancellationToken)
        {
            var date = request.Date;

            var queryable = IdentityService.Entity<TellerRegister>()
               .Where(p => p.OpenDate == date.Date &&
               p.GroupId == request.GroupId)
               .Select(p => new
               {
                   p.Id,
                   p.CloseDate,
                   //p.IsClosed,
                   p.TellerId,
                   p.OpenDate,
                   p.Teller.UserId,
                   p.GroupId,
                   //p.CashFloatTotal,
                   //p.Amount,
               })
               .GroupBy(g => new
               {
                   g.Id,
                   g.CloseDate,
                   //g.IsClosed,
                   g.TellerId,
                   g.OpenDate,
                   g.UserId,
                   g.GroupId,
                   //g.PaymentTotal,
                   //g.SubscriptionTypeName,
               })
               .Select(g => new ClosurableTellerModel
               {
                   //CashFloatTotal = g.Sum(s => s.CashFloatTotal),
                   CloseDate = g.Key.CloseDate,
                   Email = string.Empty,
                   FullName = string.Empty,
                   Id = g.Key.TellerId,
                   //IsClosed = g.Key.IsClosed,
                   OpenDate = g.Key.OpenDate,
                   //PaymentTotal = g.Key.PaymentTotal,
                   //TransactionPayment = g.Sum(s => s.Amount * s.IncomeFactor),
                   PhoneNumber = string.Empty,
                   Photo = string.Empty,
                   //SubscriptionTypeName = g.Key.SubscriptionTypeName,
                   TellerRegisterId = g.Key.Id,
                   UserId = g.Key.UserId
               });

            return Task.FromResult(queryable);
        }
    }
}
