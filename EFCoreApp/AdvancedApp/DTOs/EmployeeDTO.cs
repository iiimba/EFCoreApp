using AdvancedApp.Models;
using System;

namespace AdvancedApp.DTOs
{
    public class EmployeeDTO
    {
        public string SSN { get; set; }

        public string FirstName { get; set; }

        public string FamilyName { get; set; }

        public decimal Salary { get; set; }

        public SecondaryIdentityDTO OtherIdentity { get; set; }

        public bool SoftDeleted { get; set; }

        public DateTime LastUpdated { get; set; }

        public EmployeeDTO()
        {

        }

        public EmployeeDTO(Employee employee)
        {
            FirstName = employee.FirstName;
            SSN = employee.SSN;
            Salary = employee.Salary;
            LastUpdated = employee.LastUpdated;
            OtherIdentity = employee.OtherIdentity == null ? null : new SecondaryIdentityDTO
            {
                Name = employee.OtherIdentity.Name,
                InActiveUse = employee.OtherIdentity.InActiveUse
            };
        }
    }
}
