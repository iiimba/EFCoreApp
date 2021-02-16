using System.Threading.Tasks;

namespace AdvancedApp.Models.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee[]> GetEmployeesAsync();

        Task<Employee[]> GetEmployeesIncludeDeletedAsync();

        Task<Employee[]> GetEmployeesBySearchTermAsync(string searchTerm);

        Task InsertNewEmployeeAsync();

        Task<bool> SoftDeleteByIdAsync(string ssn);
    }
}
