using AdvancedApp.Models.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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
            var employees = await context.Employees
                .Include(e => e.OtherIdentity)
                .Select(e => new Employee
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    SSN = e.SSN,
                    OtherIdentity = e.OtherIdentity == null ? null : new SecondaryIdentity
                    {
                        Name = e.OtherIdentity.Name,
                        InActiveUse = e.OtherIdentity.InActiveUse
                    }
                })
                .ToArrayAsync();

            return employees;
        }

        public async Task<Employee[]> GetEmployeesIncludeDeletedAsync()
        {
            var employees = await context.Employees
                .Include(e => e.OtherIdentity)
                .IgnoreQueryFilters()
                .Select(e => new Employee
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    SSN = e.SSN,
                    OtherIdentity = e.OtherIdentity == null ? null : new SecondaryIdentity
                    {
                        Name = e.OtherIdentity.Name,
                        InActiveUse = e.OtherIdentity.InActiveUse
                    }
                })
                .ToArrayAsync();

            return employees;
        }

        public async Task<Employee[]> GetEmployeesBySearchTermAsync(string searchTerm)
        {
            var employees = await context.Employees
                .Include(e => e.OtherIdentity)
                .Where(e => EF.Functions.Like(e.FirstName, searchTerm))
                .Select(e => new Employee
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    SSN = e.SSN,
                    OtherIdentity = e.OtherIdentity == null ? null : new SecondaryIdentity
                    {
                        Name = e.OtherIdentity.Name,
                        InActiveUse = e.OtherIdentity.InActiveUse
                    }
                })
                .ToArrayAsync();

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

        public async Task<bool> SoftDeleteByIdAsync(string ssn)
        {
            var employee = await context.Employees.FirstOrDefaultAsync(e => e.SSN == ssn);
            if (employee == null)
            {
                return false;
            }

            employee.SoftDeleted = true;
            var isSoftDeleted = await context.SaveChangesAsync() == 1;

            return isSoftDeleted;
        }
    }
}
