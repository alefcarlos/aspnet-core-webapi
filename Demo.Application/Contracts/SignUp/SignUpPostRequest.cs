using Demo.Application.Contracts.Shared;
using Demo.Application.Shared.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.Application.Contracts.SignUp
{
    /// <summary>
    /// Contrato de criação de usuário
    /// </summary>
    public class SignUpPostRequest
    {
        /// <summary>
        /// Nome completo do usuário
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Data de nascimento
        /// </summary>
        public DateTime BornDate { get; set; }

        /// <summary>
        /// Sexo do cliente
        /// </summary>
        public EGender Gender { get; set; }

        /// <summary>
        /// Endereço completo do cliente
        /// </summary>
        public AddressRequest Address { get; set; }

        /// <summary>
        /// E-mail do usuário
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Senha do usuário
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Confirmação de senha do usuário
        /// </summary>
        public string ConfirmPassword { get; set; }
    }
}
