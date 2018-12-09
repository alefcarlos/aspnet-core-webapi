using Demo.Core.Contracts.SignIn;
using Framework.Services;

namespace Demo.Core.Services
{
    public interface ISignInServices
    {
        ServicesResult GenerateToken(SignInPostRequest request);
    }
}
