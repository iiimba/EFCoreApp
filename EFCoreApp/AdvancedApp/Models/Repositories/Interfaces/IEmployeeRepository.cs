using AdvancedApp.DTOs;
using System.Threading.Tasks;

namespace AdvancedApp.Models.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<EmployeeDTO[]> GetEmployeesAsync();

        Task<EmployeeDTO[]> GetEmployeesIncludeDeletedAsync();

        Task<EmployeeDTO[]> GetEmployeesBySearchTermAsync(string searchTerm);

        Task InsertNewEmployeeAsync();

        Task<bool> SoftDeleteByIdAsync(string ssn);

        Task<bool> UpdateAsync(EmployeeDTO employeeDTO);
    }
}
