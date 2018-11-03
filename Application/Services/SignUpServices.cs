using Application.Contracts.SignUp;
using Application.Validations.SignUp;
using Framework.Services;
using System.Linq;

namespace Application.Services
{
    /// <summary>
    /// Serivço SignIn
    /// </summary>
    public class SignUpServices : BaseServices, ISignUpServices
    {
        public ServicesResult Post(SignUpPostRequest request)
        {
            //Validar request
            var validator = new SignUpPostRequestValidator();
            var validatorResult = validator.Validate(request);

            if (!validatorResult.IsValid)
                return BadRequest(validatorResult.Errors.First().ErrorMessage);

            //Validar se usuário existe na base

            //Persistir usuário na base

            return Ok();
        }
    }
}
