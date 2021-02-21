using ApiWithToken.Domain.Response;
using ApiWithToken.Domain.Services;
using ApiWithToken.Security.Token;
using System;

namespace ApiWithToken.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly IUserService _userService;
        private readonly ITokenHandler _tokenHandler;

        public AuthenticationService(IUserService userService, ITokenHandler tokenHandler)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
        }
        public AccessTokenResponse CreateAccessToken(string email, string password)
        {
            try
            {
                var userResponse = _userService.FindEmailAndPassword(email, password);
                if (!userResponse.Success) return new AccessTokenResponse(userResponse.Message);
                var accessToken = _tokenHandler.CreateAccessToken(userResponse.User);
                _userService.SaveRefreshToken(userResponse.User.Id,accessToken.RefreshToken);
                return new AccessTokenResponse(accessToken);

            }
            catch (Exception ex)
            {
                return new AccessTokenResponse($"İşlem sırasında bir hata oluştu :{ex.Message}");
            }
        }

        public AccessTokenResponse CreateAccessTokenByRefreshToken(string refreshToken)
        {
            try
            {
                var userResponse = _userService.GetUserRefreshToken(refreshToken);
                if (userResponse.Success)
                {
                    if (!(userResponse.User.RefreshTokenEndDate > DateTime.Now))
                        return new AccessTokenResponse("RefreshTokenın süresi dolmuştur");
                    var accessToken = _tokenHandler.CreateAccessToken(userResponse.User);
                    _userService.SaveRefreshToken(userResponse.User.Id, accessToken.RefreshToken);
                    return new AccessTokenResponse(accessToken);
                }

                return new AccessTokenResponse("Böyle bir kullanıcı bulunamadı");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public AccessTokenResponse RevokeRefreshToken(string refreshToken)
        {
            var userResponse = _userService.GetUserRefreshToken(refreshToken);
            if (!userResponse.Success) return new AccessTokenResponse("Token Bulunamadı");
            _userService.RemoveRefreshToken(userResponse.User);
            return new AccessTokenResponse(new AccessToken());

        }
    }
}
