using Demo.Application.Contracts.SignUp;
using Demo.Application.Data.MongoDB.Entities;
using Demo.Application.Data.MongoDB.Repositories;
using Framework.Services;

namespace Demo.Application.Services
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
            //Validar se usuário existe na base
            //Persistir usuário na base
            var entity = new UserEntity(request);
            _userRepository.Create(entity);

            return Ok(new { entity.Id });
        }
    }
}
