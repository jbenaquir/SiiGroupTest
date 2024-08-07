using Models;
using System.Net;
using System.Text.Json.Nodes;

namespace DataAccess
{
    public class EmployeesDataAccess
    {
        private string webApiUrl = "https://dummy.restapiexample.com/api/v1";
        private string FetchEmployees()
            =>
            new WebClient().DownloadString($"{webApiUrl}/employees");

        private string FetchEmployeeById(int id) =>
            new WebClient().DownloadString($"{webApiUrl}/employee/{id}");

        public IEnumerable<Employee> GetEmployees()
        {
            var employeesJson = JsonNode.Parse(FetchEmployees());

            if (employeesJson["status"] != null
                && employeesJson["status"].ToString() != "success")
            {
                throw new Exception("Too Many Requests");
            }

            foreach (var item in employeesJson["data"].AsArray())
            {
                yield return new Employee()
                {
                    id = int.Parse(item["id"].ToString()),
                    employee_name = item["employee_name"].ToString(),
                    employee_salary = decimal.Parse(item["employee_salary"].ToString()),
                    employee_age = int.Parse(item["employee_age"].ToString()),
                    profile_image = item["profile_image"].ToString()
                };
            }
        }

        public Employee GetEmployeeById(int id)
        {
            var employeesJson = JsonNode.Parse(FetchEmployeeById(id));

            if (employeesJson["status"] != null
                && employeesJson["status"].ToString() != "success")
            {
                throw new Exception("Too Many Requests");
            }

            var data = employeesJson["data"];

            return new Employee()
            {
                id = int.Parse(data["id"].ToString()),
                employee_name = data["employee_name"].ToString(),
                employee_salary = decimal.Parse(data["employee_salary"].ToString()),
                employee_age = int.Parse(data["employee_age"].ToString()),
                profile_image = data["profile_image"].ToString()
            };
        }
    }
}