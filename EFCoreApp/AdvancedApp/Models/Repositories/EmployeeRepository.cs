using AdvancedApp.Models.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AdvancedApp.Models.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AdvancedContext context;

        public EmployeeRepository(AdvancedContext context)
        {
            this.context = context;
        }

        public async Task<Employee[]> GetEmployeesAsync()
        {
            var employees = await context.Employees.ToArrayAsync();

            return employees;
        }

        public async Task InsertNewEmployeeAsync()
        {
            var employee = new Employee
            {
                SSN = Guid.NewGuid().ToString(),
                FirstName = "Vlad",
                FamilyName = "Mis",
                Salary = 500
            };

            context.Employees.Add(employee);
            await context.SaveChangesAsync();
        }
    }
}
