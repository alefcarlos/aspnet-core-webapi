using Demo.Application.Data.MySql.Entities;
using Framework.Data.EntityFramework;

namespace Demo.Application.Data.MySql.Repositories
{
    public class CharacterRepository : EFRepositoryBase<CharacterEntity>, ICharacterRepository
    {
        public CharacterRepository(DbzMySqlContext context) : base(context)
        {
        }
    }
}
