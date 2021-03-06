﻿using AdvancedApp.DTOs;
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

        public async Task<EmployeeDTO[]> GetEmployeesAsync()
        {
            //var employeeFromSql = await context.Employees
            //    .FromSqlRaw(@"exec GetBySalary @SalaryFilter = {0}", 500)
            //    .IgnoreQueryFilters()
            //    .ToArrayAsync();

            //var employeeFromSql = await context.Employees
            //    .FromSqlRaw(@"select * from NotDeletedView e
            //                where e.Salary > {0}", 600)
            //    .Include(e => e.OtherIdentity)
            //    .ToArrayAsync();

            //var employeeFromSql = await context.Employees
            //    .FromSqlRaw(@"select * from dbo.GetSalaryTable({0})", 600)
            //    .Include(e => e.OtherIdentity)
            //    .ToArrayAsync();

            //var count = await context.Database.ExecuteSqlRawAsync("exec RestoreSoftDelete");

            var employees = await context.Employees
                .Include(e => e.OtherIdentity)
                //.OrderBy(e => EF.Property<DateTime>(e, "LastUpdated"))
                .OrderBy(e => e.LastUpdated)
                .Select(e => new EmployeeDTO(e))
                .ToArrayAsync();

            return employees;
        }

        public async Task<EmployeeDTO[]> GetEmployeesIncludeDeletedAsync()
        {
            var employees = await context.Employees
                .Include(e => e.OtherIdentity)
                .IgnoreQueryFilters()
                .Select(e => new EmployeeDTO(e))
                .ToArrayAsync();

            return employees;
        }

        public async Task<EmployeeDTO[]> GetEmployeesBySearchTermAsync(string searchTerm)
        {
            var employees = await context.Employees
                .Include(e => e.OtherIdentity)
                .Where(e => EF.Functions.Like(e.FirstName, searchTerm))
                .Select(e => new EmployeeDTO(e))
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
                Salary = 500,
                LastUpdated = DateTime.Now
            };

            //context.Entry(employee).Property("LastUpdated").CurrentValue = DateTime.Now;

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

        public async Task<bool> UpdateAsync(EmployeeDTO employeeDTO)
        {
            var employee = await context.Employees.FindAsync(employeeDTO.SSN, employeeDTO.FirstName, employeeDTO.FamilyName);
            if (employee == null)
            {
                return false;
            }

            employee.Salary = employeeDTO.Salary;

            var updated = await context.SaveChangesAsync() == 1;

            return updated;
        }

        public async Task<bool> DeleteAsync(Employee employee)
        {
            var employeeDB = await context.Employees
                .Include(e => e.OtherIdentity)
                .FirstOrDefaultAsync(e => e.SSN == employee.SSN && e.FirstName == employee.FirstName && e.FamilyName == employee.FamilyName);

            if (employeeDB == null)
            {
                return false;
            }

            context.Remove(employeeDB);
            var deleted = await context.SaveChangesAsync() > 0;

            return deleted;
        }
    }
}
