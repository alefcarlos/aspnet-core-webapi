using Demo.Core.Contracts.SignUp;
using Framework.Services;

namespace Demo.Core.Services
{
    public interface ISignUpServices
    {
        ServicesResult Post(SignUpPostRequest request);
    }
}
