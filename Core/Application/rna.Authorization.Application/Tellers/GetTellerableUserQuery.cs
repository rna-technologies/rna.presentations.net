using rna.Authorization.Application.Models;
using MediatR;
using System.Linq;

namespace rna.Authorization.Application
{
    public class GetTellerableUserQuery : IRequest<IQueryable<CustomUserModel>>
    {
        public UrlQueryParams Params { get; set; }
    }
}
