using Demo.Core.Data.MySql.Entities;
using Framework.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Core.Data.MySql.Repositories
{
    public class FamilyRepository : EFRepositoryBase<FamilyEntity>, IFamilyRepository
    {
        public FamilyRepository(DbzMySqlContext context) : base(context)
        {
        }

        public Task<List<FamilyEntity>> GetRelativesAsync(int characterId)
        {
            return Query()
                .AsNoTracking()
                .Include(x => x.Relative)
                .Where(x => x.CharacterID == characterId).ToListAsync();
        }
    }
}
