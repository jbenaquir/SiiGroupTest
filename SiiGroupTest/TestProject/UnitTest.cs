using Business;

namespace TestProject
{
    [TestClass]
    public class UnitTest
    {
        EmployeesLB _employeesLB = new EmployeesLB();

        [TestMethod]
        public void TestGetEmployee_anual_salary_sucess()
        {
            decimal anual_salary = _employeesLB.GetEmployee_anual_salary(12);

            Assert.AreEqual(144, anual_salary);
        }

        [TestMethod]
        public void TestGetEmployee_anual_salary_0()
        {
            decimal anual_salary = _employeesLB.GetEmployee_anual_salary(0);

            Assert.AreEqual(0, anual_salary);
        }
    }
}