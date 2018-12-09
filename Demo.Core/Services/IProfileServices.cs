using Framework.Services;
using System.Threading.Tasks;

namespace Demo.Core.Services
{
    public interface IProfileServices
    {
        /// <summary>
        /// Salvar imagem de perfil do usuário
        /// </summary>
        /// <param name="userId">Código do usuário</param>
        /// <param name="image"></param>
        Task<ServicesResult> SaveUserPhotoAsync(string userId, byte[] image);
    }
}
