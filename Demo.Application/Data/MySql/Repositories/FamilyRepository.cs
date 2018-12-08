using Demo.Application.Data.MySql.Entities;
using Framework.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Application.Data.MySql.Repositories
{
    public class FamilyRepository : EFRepositoryBase<FamilyEntity>, IFamilyRepository
    {
        public FamilyRepository(DbzMySqlContext context) : base(context)
        {
        }

        public List<FamilyEntity> GetRelatives(int characterId)
        {
            return Query()
                .AsNoTracking()
                .Include(x => x.Relative)
                .Where(x => x.CharacterID == characterId).ToList();
        }
    }
}
