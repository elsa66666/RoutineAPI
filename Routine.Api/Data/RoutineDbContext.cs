using Microsoft.EntityFrameworkCore;
using Routine.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Routine.Api.Data
{
    public class RoutineDbContext: DbContext
    {
        public RoutineDbContext(DbContextOptions<RoutineDbContext> options) : base(options)
        {

        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //字段约束
            modelBuilder.Entity<Company>().Property(x => x.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Company>().Property(x => x.Introduction).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<Employee>().Property(x => x.EmployeeNo).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Employee>().Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>().Property(x => x.LastName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>().HasOne(x => x.Company)
                                                                .WithMany(x => x.Employees)
                                                                .HasForeignKey(x => x.CompanyId)
                                                                .OnDelete(DeleteBehavior.Cascade); //级联删除

            //添加几个数据
            modelBuilder.Entity<Company>().HasData(
                new Company {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Name = "Microsoft",
                    Introduction = "A Great Company!"
                },
                new Company {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Name = "Dreamer",
                    Introduction = "XMX's Company!"
                },
                new Company
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    Name = "Apple",
                    Introduction = "Sell the expensive products!"
                } );

            modelBuilder.Entity<Employee>().HasData(
                new Employee {
                    Id = Guid.Parse("00000000-0000-0000-0001-000000000001"),
                    CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    DateOfBirth = new DateTime(1999, 10, 1),
                    EmployeeNo = "M001",
                    FirstName = "Lisa",
                    LastName = "King",
                    Gender = Gender.female
                },
                new Employee
                {
                    Id = Guid.Parse("00000000-0000-0000-0001-000000000002"),
                    CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    DateOfBirth = new DateTime(1998, 11, 1),
                    EmployeeNo = "M002",
                    FirstName = "Bob",
                    LastName = "Wang",
                    Gender = Gender.male
                },
                new Employee
                {
                    Id = Guid.Parse("00000000-0000-0000-0002-000000000001"),
                    CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    DateOfBirth = new DateTime(2000, 8, 25),
                    EmployeeNo = "D001",
                    FirstName = "Elsa",
                    LastName = "Shaw",
                    Gender = Gender.female
                },
                new Employee
                {
                    Id = Guid.Parse("00000000-0000-0000-0002-000000000002"),
                    CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    DateOfBirth = new DateTime(1999, 12, 1),
                    EmployeeNo = "D002",
                    FirstName = "Alisa",
                    LastName = "Li",
                    Gender = Gender.female
                },
                new Employee
                {
                    Id = Guid.Parse("00000000-0000-0000-0003-000000000001"),
                    CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    DateOfBirth = new DateTime(1998, 10, 25),
                    EmployeeNo = "A001",
                    FirstName = "Doris",
                    LastName = "Wang",
                    Gender = Gender.female
                },
                 new Employee
                  {
                     Id = Guid.Parse("00000000-0000-0000-0003-000000000002"),
                     CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                     DateOfBirth = new DateTime(1960, 11, 1),
                     EmployeeNo = "D002",
                     FirstName = "Timothy",
                     LastName = "Cook",
                     Gender = Gender.male
                  }
            );
        }
    }
}
