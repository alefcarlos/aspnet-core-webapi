using Demo.Application.Data.MySql.Entities;
using Framework.Data.EntityFramework;
using System.Collections.Generic;

namespace Demo.Application.Data.MySql.Repositories
{
    public interface ICharacterRepository :  IEFRepository<CharacterEntity>
    {
    }
}
