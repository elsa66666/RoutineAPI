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
    [Route("api/companies")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompaniesController(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<List<CompanyDTO>>> GetCompanies()
        {
            var companies = await _companyRepository.GetCompaniesAsync(); //包含所有公司的list
            var companyDtos = _mapper.Map<List<CompanyDTO>>(companies);
            return Ok(companyDtos);
        }

        [HttpGet("{companyId}", Name = nameof(GetCompany))]  //api/companies/{companyId}
        public async Task<ActionResult<CompanyDTO>> GetCompany(Guid companyId)
        {
            var company = await _companyRepository.GetCompanyAsync(companyId);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CompanyDTO>(company));
        }

        [HttpPost]
        public async Task<ActionResult<CompanyDTO>> CreateCompany(CompanyAddDTO company)
        {
            var entity = _mapper.Map<Company>(company);      //将CompanyAddDTO转化成Company实体
            _companyRepository.AddCompany(entity);
            await _companyRepository.SaveAsync();

            var returnDto = _mapper.Map<CompanyDTO>(entity);
            return CreatedAtRoute(nameof(GetCompany), new { companyId = returnDto.Id }, returnDto);
        }

       [HttpDelete("{companyId}")]
       public async Task<IActionResult> DeleteCompany(Guid companyId)
        {
            var companyEntity = await _companyRepository.GetCompanyAsync(companyId);
            if (companyEntity == null)
            {
                return NotFound();
            }
            await _companyRepository.GetEmployeesAsync(companyId, null, null);
            _companyRepository.DeleteCompany(companyEntity);
            await _companyRepository.SaveAsync();
            return NoContent();
        }
    }
}
