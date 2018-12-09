using Demo.Core.Contracts.Shared;
using Demo.Core.Shared.Enum;
using System;

namespace Demo.Core.Contracts.SignIn
{
    /// <summary>
    /// Contrato de criação de usuário
    /// </summary>
    public class SignInPostRequest
    {
        /// <summary>
        /// E-mail do usuário
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Senha do usuário
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Nível de obtenção de token
        /// password - Gerar um novo token
        /// refresh_token - Gera um novo token através de um refresh_token
        /// </summary>
        public string GrantType { get; set; }

        /// <summary>
        /// Refresh token para tentar ser revalidado
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
