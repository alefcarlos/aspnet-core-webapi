using Application.Contracts.SignIn;
using Framework.Services;
using Framework.WebAPI.Hosting.JWT;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Application.Services
{
    /// <summary>
    /// Serivço SignIn
    /// </summary>
    public class SignInServices : BaseServices, ISignInServices
    {
        private readonly SigningConfigurations _signingConfigurations;
        private readonly TokenConfigurations _tokenConfigurations;

        public SignInServices(SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations)
        {
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }

        public ServicesResult Login(SignInPostRequest request)
        {
            //Validar request

            if (request.GrantType == "refresh_token")
            {
                // Lookup which user is tied to model.RefreshToken
                // Generate access token from the username (no password check required)
                // Return the token (access + expiration)
                return Ok();
            }
            else if (request.GrantType == "password")
            {
                //Validar dados

                //Gerar Token
                var identity = new ClaimsIdentity(
                new GenericIdentity("123", "Login"),
                new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, "123")
                });

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = _tokenConfigurations.Issuer,
                    Audience = _tokenConfigurations.Audience,
                    SigningCredentials = _signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                return Ok(new
                {
                    authenticated = true,
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    refreshToken = Guid.NewGuid().ToString()
                });
            }

            return BadRequest("Request inválido");
        }
    }
}
