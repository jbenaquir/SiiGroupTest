using Business;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Models;
using SiiGroupTest.Models;
using System.Diagnostics;
using System.Numerics;

namespace SiiGroupTest.Controllers
{
    public class HomeController : Controller
    {
        EmployeesDataAccess _employeesDA;
        EmployeesLB _employeesLB;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _employeesDA = new EmployeesDataAccess();
            _employeesLB = new EmployeesLB();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetEmployees(int? idEmployee = null)
        {
            string errorMessage = string.Empty;
            List<Employee> employees = new List<Employee>();
            try
            {
                if (idEmployee == null)
                {
                    employees = _employeesDA.GetEmployees().ToList();
                }
                else
                {
                    employees = new List<Employee>()
                    {
                        _employeesDA.GetEmployeeById(idEmployee.Value)
                    };
                }

            }
            catch (Exception)
            {
                errorMessage = "Too Many Requests, please try until API answer propperly or await...";
            }

            return Ok(new {
                employees = employees.Select(employee => new Employee() { 
                    id = employee.id,
                    employee_name = employee.employee_name,
                    employee_salary = employee.employee_salary,
                    employee_age = employee.employee_age,
                    employee_anual_salary = _employeesLB.GetEmployee_anual_salary(employee.employee_salary)
                }),
                errorMessage
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}