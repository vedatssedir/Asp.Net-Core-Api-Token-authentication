using ApiWithToken.Domain.Services;
using ApiWithToken.Extensions;
using ApiWithToken.Resources;
using Microsoft.AspNetCore.Mvc;

namespace ApiWithToken.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost]
        public IActionResult AccessToken(LoginResource loginResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            var accessTokenResponse = _authenticationService.CreateAccessToken(loginResource.Email, loginResource.Password);
            if (accessTokenResponse.Success)
            {
                return Ok(accessTokenResponse.AccessToken);
            }
            return BadRequest(accessTokenResponse.Message);
        }

        [HttpPost]
        public IActionResult RefreshToken(TokenResource tokenResource)
        {
            var accessTokenResponse =
                _authenticationService.CreateAccessTokenByRefreshToken(tokenResource.RefreshToken);
            if (accessTokenResponse.Success)
            {
                return Ok(accessTokenResponse.AccessToken);
            }

            return BadRequest(accessTokenResponse.Message);
        }

        [HttpPost]
        public IActionResult RemoveRefreshToken(TokenResource tokenResource)
        {

            var accessTokenResponse =
                _authenticationService.RevokeRefreshToken(tokenResource.RefreshToken);
            if (accessTokenResponse.Success)
            {
                return Ok(accessTokenResponse.AccessToken);
            }

            return BadRequest(accessTokenResponse.Message);

        }

    }
}
