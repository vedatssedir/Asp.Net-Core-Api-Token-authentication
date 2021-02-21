using ApiWithToken.Domain.Entities;

namespace ApiWithToken.Security.Token
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(User user);
        void RevokeRefreshToken(User user);
    }
}