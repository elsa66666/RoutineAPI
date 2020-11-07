using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Routine.Api.Entities;
using Routine.Api.Models;
using Routine.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Controllers
{
    [ApiController]
    [Route("api/companies/{companyId}/employees")]
    public class EmployeesController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public EmployeesController(IMapper mapper, ICompanyRepository companyRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployeesForCompany
            (Guid companyId, [FromQuery] string genderDisplay, string q)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }
            var employees = await _companyRepository.GetEmployeesAsync(companyId, genderDisplay, q);
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);
            return Ok(employeeDtos);
        }

            [HttpGet("{employeeId}", Name = nameof(GetEmployeeForCompany))]
            public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployeeForCompany(Guid companyId, Guid employeeId)
            {
                if (!await _companyRepository.CompanyExistsAsync(companyId))
                {
                    return NotFound();
                }
                var employee = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
            if (employee == null)
            {
                return NotFound();
            }
                var employeeDto = _mapper.Map<EmployeeDTO>(employee);
                return Ok(employeeDto);
            }

        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> CreateEmployeeForCompany(Guid companyId, EmployeeAddDTO employee)
        {
            if (! await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }
            var entity = _mapper.Map<Employee>(employee);

            _companyRepository.AddEmployee(companyId, entity);
            await _companyRepository.SaveAsync();

            var dtoToReturn = _mapper.Map<EmployeeDTO>(entity);

            return CreatedAtRoute(nameof(GetEmployeeForCompany), new
            {
                companyId = companyId,
                employeeId = dtoToReturn.Id
            }, dtoToReturn);
        }

        [HttpPut("{employeeId}")]
        public async Task<IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid employeeId, EmployeeUpdateDTO employee)
        {
            if (! await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }
            var employeeEntity = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
            if (employeeEntity == null)
            {
                return NotFound();
            }
            //entity转化为updateDTO
            //将传入的employee更新到updateDTO
            //把updateDTO映射回entity
            _mapper.Map(employee, employeeEntity);
            _companyRepository.UpdateEmployee(employeeEntity);
            await _companyRepository.SaveAsync();
            return NoContent();
        }

        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid employeeId)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }
            var employeeEntity = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
            if (employeeEntity == null)
            {
                return NotFound();
            }

            _companyRepository.DeleteEmployee(employeeEntity);
            await _companyRepository.SaveAsync();
            return NoContent();
        }
    
    
    }
}
