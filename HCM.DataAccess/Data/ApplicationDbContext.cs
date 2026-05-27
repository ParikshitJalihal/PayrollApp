using HCM.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<LedgerEntry> LedgerEntries { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Requesites> Requesites { get; set; }
        public DbSet<RequisiteDetails> RequisiteDetails { get; set; }
        public DbSet<PayComponent> PayComponents { get; set; }

        public DbSet<TimeSheetEntry> TimeSheetEntries { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Jobs>().HasData(
                new Jobs { JobId = 1, JobName = "Software Engineer", JobStatus = true, JobStatusDescription = "Open" },
                new Jobs { JobId = 2, JobName = "Data Analyst", JobStatus = true, JobStatusDescription = "Open" },
                new Jobs { JobId = 3, JobName = "Project Manager", JobStatus = false, JobStatusDescription = "Closed" },
                new Jobs { JobId = 4, JobName = "UX Designer", JobStatus = true, JobStatusDescription = "Open" }
                );

            modelBuilder.Entity<Requesites>().HasData(
                new Requesites { ReqId = 1, ReqName = "Assets", ReqDescription = "" },
                new Requesites { ReqId = 2, ReqName = "Department", ReqDescription = "" },
                new Requesites { ReqId = 3, ReqName = "Designation", ReqDescription = "" },
                new Requesites { ReqId = 4, ReqName = "Gender", ReqDescription = "" }
                );

            modelBuilder.Entity<RequisiteDetails>().HasData(
                new RequisiteDetails { RequisiteDetailsId = 1, ReqId = 1, RequisiteValue = "Laptop" },
                new RequisiteDetails { RequisiteDetailsId = 2, ReqId = 1, RequisiteValue = "Mouse" },
                new RequisiteDetails { RequisiteDetailsId = 3, ReqId = 2, RequisiteValue = "HR" },
                new RequisiteDetails { RequisiteDetailsId = 4, ReqId = 2, RequisiteValue = "Finance" },
                new RequisiteDetails { RequisiteDetailsId = 5, ReqId = 2, RequisiteValue = "Operations" },
                new RequisiteDetails { RequisiteDetailsId = 6, ReqId = 2, RequisiteValue = "Technical" })
                   ;


            modelBuilder.Entity<Account>()
           .HasMany(a => a.LedgerEntries)
           .WithOne(le => le.Account)
           .HasForeignKey(le => le.AccountId);

            modelBuilder.Entity<PayComponent>().HasData(
     new PayComponent
     {
         PayComponentId = 1,
         ComponentName = "Basic Salary",
         ComponentType = "Earning",
         PayFormula = "#Basic#",
         CreatedDate = new DateTime(2026, 05, 18, 20, 00, 00, DateTimeKind.Local),
         ModifiedDate = null
     },
     new PayComponent
     {
         PayComponentId = 2,
         ComponentName = "House Rent Allowance",
         ComponentType = "Earning",
         PayFormula = "##BASIC#*#50#%#",
         CreatedDate = new DateTime(2026, 05, 18, 20, 00, 00, DateTimeKind.Local),
         ModifiedDate = null
     },
     new PayComponent
     {
         PayComponentId = 3,
         ComponentName = "Medical Allowance",
         ComponentType = "Earning",
         PayFormula = "#",
         CreatedDate = new DateTime(2026, 05, 18, 20, 00, 00, DateTimeKind.Local),
         ModifiedDate = null
     },
     new PayComponent
     {
         PayComponentId = 4,
         ComponentName = "Provident Fund",
         ComponentType = "Deduction",
         PayFormula = "#",
         CreatedDate = new DateTime(2026, 05, 18, 20, 00, 00, DateTimeKind.Local),
         ModifiedDate = null
     },
     new PayComponent
     {
         PayComponentId = 5,
         ComponentName = "Professional Tax",
         ComponentType = "Deduction",
         PayFormula = "#",
         CreatedDate = new DateTime(2026, 05, 18, 20, 00, 00, DateTimeKind.Local),
         ModifiedDate = null
     }
 );

        }
    }
}
