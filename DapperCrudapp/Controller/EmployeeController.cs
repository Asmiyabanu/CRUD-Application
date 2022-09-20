using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace DapperCrudapp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _config;

        public EmployeeController(IConfiguration config)
        {
            _config = config;

        }
        [HttpGet]
        public async Task<ActionResult<List<EmployeeController>>> GetAllEmployee()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

           IEnumerable<Employee> employees = await SelectAllEmbloyees(connection);
            return Ok(employees);
        }

        [HttpGet("{employeeid}")]
        public async Task<ActionResult<Employee>> GetEmployee(int employeeid)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var employe = await connection.QueryFirstAsync<EmployeeTable>("select * from EmbloyeeTable where id = @id",
                new {id = employeeid});
            return Ok(employe);
        }

        [HttpPost]
        public async Task<ActionResult<List<Employee>>> CreateEmployee(Employee employee)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            await connection.ExecuteAsync("insert in to Employees(firstname,lastname,place) values (@firstname,@lastname,@place)", employee);
            return Ok(await SelectAllEmployees(connection));
        }

        [HttpPut]
        public async Task<ActionResult<List<Employee>>> updateEmployee(Employee employee)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            await connection.ExecuteAsync("update Employees set firstname = @firstname,lastname = @ lastname,place = @place where id = id", employee);
            return Ok(await SelectAllEmployees(connection));
        }

        [HttpDelete]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int heroid)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            await connection.ExecuteAsync("delete from Employees where id = @id",new { id = employeeid });
            return Ok(await SelectAllEmployees(connection));
        }

        private static async Task<IEnumerable<Employee>>SelectAllEmbloyees(SqlConnection connection)
        {
            return await connection.QueryAsync<EmployeeTable>("select * from EmbloyeeTable");
        }

    }
    }

