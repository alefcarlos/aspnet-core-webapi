using Demo.Application.Contracts.SignUp;
using Framework.Services;

namespace Demo.Application.Services
{
    public interface ISignUpServices
    {
        ServicesResult Post(SignUpPostRequest request);
    }
}
