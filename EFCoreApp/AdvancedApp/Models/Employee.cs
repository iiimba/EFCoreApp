using System;

namespace AdvancedApp.Models
{
    public class Employee
    {
        private decimal databaseSalary;

        public long Id { get; set; }

        public string SSN { get; set; }

        public string FirstName { get; set; }

        public string FamilyName { get; set; }

        public decimal Salary 
        {
            get { return databaseSalary * 2; }
            set { databaseSalary = Math.Max(0, value); }
        }

        public SecondaryIdentity OtherIdentity { get; set; }

        public bool SoftDeleted { get; set; }
    }
}
