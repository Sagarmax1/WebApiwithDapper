using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using DapperAPI.Models;
using System.Linq;

namespace DapperApi.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly IConfiguration _config;

        public EmployeeRepository(IConfiguration config)
        {
            this._config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new MySqlConnection(_config.GetConnectionString("MyConnectionString"));
            }
        }
        public int AddEmployee(Employee Emp)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                IDbTransaction SQLTrans = conn.BeginTransaction();
                var param = new DynamicParameters(); //Most import class in dapper
                param.Add("Firstname", Emp.Firstname);
                param.Add("Lastname", Emp.Lastname);
                param.Add("DateOfBirth", Emp.DateOfBirth);
                var result = conn.Execute("AddEmployee", param, SQLTrans, 0, CommandType.StoredProcedure);
                if (result > 0)
                {
                    SQLTrans.Commit();
                }
                else
                {
                    SQLTrans.Rollback();
                }

                return result;
            }
        }

        public async Task<List<Employee>> GetByDateOfBirth(DateTime dateOfBirth)
        {
            using (IDbConnection conn = Connection)
            {
                string query = "Select ID ,Firstname,lastname,DateOfBirth from employee where DateOfBirth = @dateOfBirth";
                conn.Open();
                var result = await conn.QueryAsync<Employee>(query, new { DateOfBirth = dateOfBirth });
                return result.ToList();

            }
        }

        public async Task<Employee> GetEmployeeById(int Id)
        {
            using (IDbConnection conn = Connection)
            {
                string query = "Select ID ,Firstname,lastname,DateOfBirth from employee where Id = @Id";
                conn.Open();
                var result = await conn.QueryAsync<Employee>(query, new { Id = Id });
                return result.FirstOrDefault();

            }
        }
    }
}