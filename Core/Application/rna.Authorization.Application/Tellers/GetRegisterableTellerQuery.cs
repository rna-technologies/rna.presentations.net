using System;
using MediatR;
using System.Linq;
using rna.Authorization.Application.Models;

namespace rna.Authorization.Application
{
    //using Resource.Infrastructure.DbSetModels;
    public class GetRegisterableTellerQuery : IRequest<IQueryable<RegisterableTellerModel>>
    {
        public DateTime Date { get; set; }
        public UrlQueryParams Params { get; set; }
    }
}
