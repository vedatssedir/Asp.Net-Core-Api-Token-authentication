using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using ApiWithToken.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace ApiWithToken.Security.Token
{
    public class TokenHandlers : ITokenHandler
    {
        private readonly TokenOptions _tokenOptions;

        public TokenHandlers(IOptions<TokenOptions> tokenOptions)
        {
            _tokenOptions = tokenOptions.Value;
        }

        public AccessToken CreateAccessToken(User user)
        {
            var accessTokenExpirations = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SignHandler.GetSecurityKey(_tokenOptions.SecurityKey);
            var signCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var jwtSecurityToken = new JwtSecurityToken(_tokenOptions.Issuer, _tokenOptions.Audience,
                expires: accessTokenExpirations, notBefore: DateTime.Now,
                signingCredentials: signCredentials, claims: GetClaim(user));

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);
            var accessToken = new AccessToken
            {
                Token = token,
                RefreshToken = CreateRefreshToken(),
                Expiration = accessTokenExpirations
            };
            return accessToken;
        }

        private string CreateRefreshToken()
        {
            var numberBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(numberBytes);
            return Convert.ToBase64String(numberBytes);
        }

        private IEnumerable<Claim> GetClaim(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, $"{user.Name} {user.Surname}"),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            return claims;
        }

        public void RevokeRefreshToken(User user)
        {
            user.RefreshToken = null;
        }
    }
}