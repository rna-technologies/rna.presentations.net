namespace rna.Authentication.api.Models
{
    public class ExternalLoginModel
    {
        public string ClientId { get; set; }
        public ExternalAuthorizationUser UserData { get; set; }
    }
}