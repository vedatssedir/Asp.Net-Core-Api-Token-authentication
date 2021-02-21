using ApiWithToken.Security.Token;

namespace ApiWithToken.Domain.Response
{
    public class AccessTokenResponse : BaseResponse
    {
        public AccessToken AccessToken { get; set; }

        private AccessTokenResponse(bool success, string message, AccessToken accessToken) : base(success, message)
        {
            AccessToken = accessToken;
        }


        public AccessTokenResponse(AccessToken accessToken) : this(true, string.Empty, accessToken)
        {

        }

        public AccessTokenResponse(string message) : this(false, message, null)
        {

        }


    }
}
