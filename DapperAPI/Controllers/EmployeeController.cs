using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using DapperAPI.Models;
using DapperApi.Repository;

namespace DapperApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        IEmployeeRepository employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpPost("CreateEmployee")]
        public int PostEmployee(Employee employee)
        {
            return employeeRepository.AddEmployee(employee);
        }

        [HttpGet]
        [Route("dob/{DateOfBirth}")]
        public async Task<ActionResult<List<Employee>>> GetByDateOfBirth(DateTime DateOfBirth)
        {
           
            return await employeeRepository.GetByDateOfBirth(DateOfBirth);
        }

        [HttpGet]
        [Route("ID/{Id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int Id)
        {
            return await employeeRepository.GetEmployeeById(Id);
        }
    }
}