using Demo.Application.Data.MySql.Entities;
using Framework.Data.EntityFramework;

namespace Demo.Application.Data.MySql.Repositories
{
    public interface ICharacterRepository : IEFRepository<CharacterEntity>
    {
    }
}
