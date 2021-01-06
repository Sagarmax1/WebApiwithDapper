
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System;
using DapperAPI.Models;

namespace DapperApi.Repository
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeById(int Id);

        Task<List<Employee>> GetByDateOfBirth(DateTime dateOfBirth);
        int AddEmployee(Employee Emp);

    }
}