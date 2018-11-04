using Application.Contracts.SignUp;
using Application.Validations.SignUp;
using Clinfy.Application.Data.MongoDB.Entities;
using Clinfy.Application.Data.MongoDB.Repositories;
using Framework.Services;
using System.Linq;

namespace Application.Services
{
    /// <summary>
    /// Serivço SignIn
    /// </summary>
    public class SignUpServices : BaseServices, ISignUpServices
    {
        private readonly IUserRepository _userRepository;

        public SignUpServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ServicesResult Post(SignUpPostRequest request)
        {
            //Validar request
            var validator = new SignUpPostRequestValidator();
            var validatorResult = validator.Validate(request);

            if (!validatorResult.IsValid)
                return BadRequest(validatorResult.Errors.First().ErrorMessage);

            //Validar se usuário existe na base
            //Persistir usuário na base
            var entity = new UserEntity(request);
            _userRepository.Create(entity);

            return Ok(new { entity.Id });
        }
    }
}
