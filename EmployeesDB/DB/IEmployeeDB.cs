using System.Collections.Generic;
using EmployeesDB.Employees;

namespace EmployeesDB.DB
{
    public interface IEmployeeDB
    {
        bool InsertEmployee(Employee employee);
        bool RemoveEmployeeById(int id);

        Employee GetEmployeeById(int id);
        IList<Employee> Employees { get; }
        IList<Employee> GetEmployeesByPosition(string position);
        IList<Employee> GetEmployeesByName(string name);
    }
}
