using Demo.Core.Data.MySql.Entities;
using Framework.Data.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Core.Data.MySql.Repositories
{
    public interface ICharacterRepository : IEFRepository<CharacterEntity>
    {
    }
}
