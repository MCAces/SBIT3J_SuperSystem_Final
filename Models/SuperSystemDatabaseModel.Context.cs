﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SBIT3J_SuperSystem_Final.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DatabaseConnectionEntities : DbContext
    {
        public DatabaseConnectionEntities()
            : base("name=DatabaseConnectionEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<EmployeeAccount> EmployeeAccounts { get; set; }
        public virtual DbSet<EmployeeInformation> EmployeeInformations { get; set; }
        public virtual DbSet<AuditTrail> AuditTrails { get; set; }
        public virtual DbSet<Deduction> Deductions { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<Employee_Attendance> Employee_Attendance { get; set; }
        public virtual DbSet<Expens> Expenses { get; set; }
        public virtual DbSet<Insurance> Insurances { get; set; }
        public virtual DbSet<Leave_Request> Leave_Request { get; set; }
        public virtual DbSet<Loss_Damages> Loss_Damages { get; set; }
        public virtual DbSet<Loss_Fraud> Loss_Fraud { get; set; }
        public virtual DbSet<Pag_Ibig> Pag_Ibig { get; set; }
        public virtual DbSet<Payroll> Payrolls { get; set; }
        public virtual DbSet<Philhealth> Philhealths { get; set; }
        public virtual DbSet<Product_Info> Product_Info { get; set; }
        public virtual DbSet<Restock> Restocks { get; set; }
        public virtual DbSet<Restock_Detail> Restock_Detail { get; set; }
        public virtual DbSet<Return_Item> Return_Item { get; set; }
        public virtual DbSet<Salary> Salaries { get; set; }
        public virtual DbSet<Sales_Transaction> Sales_Transaction { get; set; }
        public virtual DbSet<Sales_Transaction_Details> Sales_Transaction_Details { get; set; }
        public virtual DbSet<SSS> SSSes { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Tax> Taxes { get; set; }
    }
}
