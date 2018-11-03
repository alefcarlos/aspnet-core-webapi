using Application.Contracts.SignUp;
using Framework.Services;

namespace Application.Services
{
    public interface ISignUpServices
    {
        ServicesResult Post(SignUpPostRequest request);
    }
}
