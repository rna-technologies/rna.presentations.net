using rna.Authorization.Application.Models;
using MediatR;

namespace rna.Authorization.Application
{
    public class GetTellerableUserPage : IRequest<PaginationInfo<CustomUserModel>>
    {
        public UrlQueryParams Params { get; set; }
    }
}
