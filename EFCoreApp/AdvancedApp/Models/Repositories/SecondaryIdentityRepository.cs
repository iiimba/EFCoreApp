using AdvancedApp.Models.Repositories.Interfaces;
using System.Threading.Tasks;

namespace AdvancedApp.Models.Repositories
{
    public class SecondaryIdentityRepository : ISecondaryIdentityRepository
    {
        private readonly AdvancedContext context;

        public SecondaryIdentityRepository(AdvancedContext context)
        {
            this.context = context;
        }

        public async Task<bool> InsertOrUpdateAsync(SecondaryIdentity identity)
        {
            context.SecondaryIdentities.Update(identity);
            var updated = await context.SaveChangesAsync() == 1;

            return updated;
        }

        public async Task<bool> UpdateAsync(SecondaryIdentity identity)
        {
            if (identity.Id <= 0)
            {
                return false;
            }

            var identityDB = await context.SecondaryIdentities.FindAsync(identity.Id);

            identityDB.Name = identity.Name;
            identityDB.InActiveUse = identity.InActiveUse;
            identityDB.PrimarySSN = identity.PrimarySSN;
            identityDB.PrimaryFirstName = identity.PrimaryFirstName;
            identityDB.PrimaryFamilyName = identity.PrimaryFamilyName;
            identityDB.PrimaryIdentity = identity.PrimaryIdentity;

            var updated = await context.SaveChangesAsync() == 1;

            return updated;
        }

        public async Task<bool> InsertOrUpdateRangeAsync(SecondaryIdentity[] identities)
        {
            context.SecondaryIdentities.UpdateRange(identities);
            var updated = await context.SaveChangesAsync() > 1;

            return updated;
        }
    }
}
