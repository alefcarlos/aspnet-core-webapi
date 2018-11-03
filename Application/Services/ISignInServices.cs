using Application.Contracts.SignIn;
using Framework.Services;

namespace Application.Services
{
    public interface ISignInServices
    {
        ServicesResult Login(SignInPostRequest request);
    }
}
