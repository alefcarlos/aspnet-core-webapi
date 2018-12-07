using Demo.Application.Contracts.SignIn;
using Framework.Services;

namespace Demo.Application.Services
{
    public interface ISignInServices
    {
        ServicesResult GenerateToken(SignInPostRequest request);
    }
}
