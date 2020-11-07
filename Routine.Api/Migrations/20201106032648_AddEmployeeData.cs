using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Routine.Api.Migrations
{
    public partial class AddEmployeeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(1999, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "M001", "Lisa", 2, "King" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("00000000-0000-0000-0001-000000000002"), new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(1998, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "M002", "Bob", 1, "Wang" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("00000000-0000-0000-0002-000000000001"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2000, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "D001", "Elsa", 2, "Shaw" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("00000000-0000-0000-0002-000000000002"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(1999, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "D002", "Alisa", 2, "Li" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("00000000-0000-0000-0003-000000000001"), new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(1998, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "A001", "Doris", 2, "Wang" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("00000000-0000-0000-0003-000000000002"), new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(1960, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "D002", "Timothy", 1, "Cook" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0001-000000000001"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0001-000000000002"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0002-000000000001"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0002-000000000002"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0003-000000000001"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0003-000000000002"));
        }
    }
}
