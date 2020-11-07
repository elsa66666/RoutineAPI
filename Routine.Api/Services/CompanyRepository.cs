using Microsoft.EntityFrameworkCore;
using Routine.Api.Data;
using Routine.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Services
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly RoutineDbContext _context;   //注入数据库
        public CompanyRepository(RoutineDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        //获取所有公司
        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            return await _context.Companies.ToListAsync();
        }
        //通过ID获取单个公司
        public async Task<Company> GetCompanyAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            return await _context.Companies.FirstOrDefaultAsync(x => x.Id == companyId);
        }
        //通过一个集合的ID获得多个companies
        public async Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds)
        {
            if (companyIds == null)
            {
                throw new ArgumentNullException(nameof(companyIds));
            }
            return await _context.Companies.Where(x => companyIds.Contains(x.Id)).OrderBy(x => x.Name).ToListAsync();
        }
       //添加company
        public void AddCompany(Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }
            company.Id = Guid.NewGuid();
            if (company.Employees != null)
            {
                foreach (var employee in company.Employees)
                {
                    employee.Id = Guid.NewGuid();
                }
            }
            _context.Companies.Add(company);
        }
        //删除company
        public void DeleteCompany(Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }
            _context.Companies.Remove(company);
        }
        //通过ID判断公司是否存在
        public async Task<bool> CompanyExistsAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            return await _context.Companies.AnyAsync(x => x.Id == companyId);
        }

        //通过公司ID获取所有员工
        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, string genderDisplay, string q)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            if (string.IsNullOrWhiteSpace(genderDisplay) && string.IsNullOrWhiteSpace(q))  //过滤条件
            {
                return await _context.Employees.Where(x => x.CompanyId == companyId).OrderBy(x => x.EmployeeNo).ToListAsync();
            }
            var items = _context.Employees.Where(x => x.CompanyId == companyId);

            if (!string.IsNullOrWhiteSpace(genderDisplay))  //过滤
            {
                genderDisplay = genderDisplay.Trim();
                var gender = Enum.Parse<Gender>(genderDisplay);
                items = items.Where(x => x.Gender == gender);
            }
            if (!string.IsNullOrWhiteSpace(q))  //搜索
            {
                q = q.Trim();
                items = items.Where(x => x.EmployeeNo.Contains(q) 
                                                        || x.FirstName.Contains(q)
                                                        || x.LastName.Contains(q));
            }
            return await items.OrderBy(x => x.EmployeeNo).ToListAsync();
        }
        //获取公司某个员工
        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid emplyeeId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            if (emplyeeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(emplyeeId));
            }
            return await _context.Employees.Where(x => x.CompanyId == companyId && x.Id == emplyeeId).FirstOrDefaultAsync();
        }
        //添加员工
        public void AddEmployee(Guid companyId, Employee emplyee)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            if (emplyee == null)
            {
                throw new ArgumentNullException(nameof(emplyee));
            }
            emplyee.CompanyId = companyId;
            _context.Employees.Add(emplyee);
        }
        public void DeleteEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
        }
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public void UpdateEmployee(Employee employee)
        {
        }
    }
}
