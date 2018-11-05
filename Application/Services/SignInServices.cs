using Application.Contracts.SignIn;
using Clinfy.Application.Data.MongoDB.Entities;
using Clinfy.Application.Data.MongoDB.Repositories;
using Clinfy.Application.Validations.SignIn;
using Framework.Services;
using Framework.WebAPI.Hosting.JWT;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
        private readonly IUserRepository _userRepository;

        public SignInServices(SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations, IUserRepository userRepository)
        {
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
            _userRepository = userRepository;
        }

        public ServicesResult GenerateToken(SignInPostRequest request)
        {
            //Validar request
            var validator = new SignInPostRequestValidator();
            var validatorResult = validator.Validate(request);
            if (!validatorResult.IsValid)
                return BadRequest(validatorResult.Errors.First().ErrorMessage);

            UserEntity userEntity;

            if (request.GrantType == "refresh_token")
            {
                //Obter usuário
                userEntity = _userRepository.GetByRefreshToken(request.RefreshToken);
                if (userEntity == null)
                    return NotFound("RefreshToken informado é inválido");

                // Gerar token
                return Ok(GenerateToken(userEntity, true));
            }

            //Pesquisar usuário
            userEntity = _userRepository.GetByEmail(request.Email);
            if (userEntity == null)
                return NotFound("Usuário não cadastrado");

            // Gerar token
            return Ok(GenerateToken(userEntity, false));
        }

        private object GenerateToken(UserEntity userEntity, bool isRefreshToken)
        {
            //Gerar Token
            Claim[] claims = new[]
            {
                new Claim("user_id", userEntity.Id.ToString()),
                new Claim(ClaimTypes.Email, userEntity.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
            };

            var identity = new ClaimsIdentity(claims, "Token");

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

            if (!isRefreshToken)
            {
                var refreshToken = Guid.NewGuid().ToString();

                //Persistir refresh token para esse usuário
                userEntity.RefreshToken = refreshToken;
                _userRepository.Update(userEntity);

                return new
                {
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    refreshToken
                };
            }

            return new
            {
                created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token
            };
        }
    }
}
