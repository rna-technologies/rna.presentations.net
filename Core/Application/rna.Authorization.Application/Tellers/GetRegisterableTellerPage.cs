using System;
using rna.Authorization.Application.Models;
using MediatR;

namespace rna.Authorization.Application
{
    //using Resource.Infrastructure.DbSetModels;
    public class GetRegisterableTellerPage : IRequest<PaginationInfo<RegisterableTellerModel>>
    {
        public DateTime? Date { get; set; }
        public UrlQueryParams Params { get; set; }
    }
}
