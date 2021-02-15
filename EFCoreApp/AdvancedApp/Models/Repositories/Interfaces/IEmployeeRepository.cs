using System.Threading.Tasks;

namespace AdvancedApp.Models.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee[]> GetEmployeesAsync();
    }
}
