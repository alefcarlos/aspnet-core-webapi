using Demo.Application.Data.MySql.Entities;
using Framework.Data.EntityFramework;
using System.Collections.Generic;

namespace Demo.Application.Data.MySql.Repositories
{
    public interface IFamilyRepository : IEFRepository<FamilyEntity>
    {
        /// <summary>
        /// Obtém todos os parentes de um determinado personagem
        /// </summary>
        /// <param name="characterId">ID do personagem.</param>
        List<FamilyEntity> GetRelatives(int characterId);
    }
}
