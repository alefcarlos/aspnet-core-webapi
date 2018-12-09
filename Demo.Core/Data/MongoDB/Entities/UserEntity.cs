using Demo.Core.Contracts.SignUp;
using Demo.Core.Shared.Enum;
using Framework.Data.MongoDB;
using System;

namespace Demo.Core.Data.MongoDB.Entities
{
    public class UserEntity : MongoEntityBase
    {
        public UserEntity(SignUpPostRequest request)
        {
            Name = request.Name;
            BornDate = request.BornDate;
            Gender = request.Gender;
            Email = request.Email;
            Password = request.Password;
        }

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
        /// E-mail do usuário
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Senha do usuário
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Token a ser utilizado para renovar sessão
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
