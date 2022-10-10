using MediatR;


namespace rna.Authorization.Application
{
    public class GetTellerableUser : IRequest<UserModel>
    {
        public string UserId { get; set; }
        public UrlQueryParams Params { get; set; }
    }
}
