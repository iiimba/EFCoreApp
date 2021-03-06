﻿using System;

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
            get { return databaseSalary; }
            set { databaseSalary = value; }
        }

        public SecondaryIdentity OtherIdentity { get; set; }

        public bool SoftDeleted { get; set; }

        public DateTime LastUpdated { get; set; }

        public byte[] RowVersion { get; set; }

        public string GeneratedValue { get; set; }

        public string NChar { get; set; }
    }
}
