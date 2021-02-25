using System.Threading.Tasks;

namespace AdvancedApp.Models.Repositories.Interfaces
{
    public interface ISecondaryIdentityRepository
    {
        Task<bool> InsertOrUpdateAsync(SecondaryIdentity identity);

        Task<bool> UpdateAsync(SecondaryIdentity identity);

        Task<bool> InsertOrUpdateRangeAsync(SecondaryIdentity[] identities);
    }
}
