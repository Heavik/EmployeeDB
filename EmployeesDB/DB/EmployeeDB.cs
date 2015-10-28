using System;
using System.Collections.Generic;
using System.IO;
using EmployeesDB.Employees;
using EmployeesDB.Serialization;

namespace EmployeesDB.DB
{
    public class EmployeeDB : IEmployeeDB
    {

        private const string ConfigPath = @"config\options.ini";

        private ISerialize serialize;

        private int currentId;

        private IList<Employee> employees;

        public EmployeeDB()
        {
            InitDb();
            LoadEmployeeList();
        }

        public IList<Employee> Employees { get { return employees; } }

        private void InitDb()
        {   
            using (var reader = new StreamReader(File.Open(ConfigPath, FileMode.Open)))
            {
                string option = reader.ReadToEnd().ToLower();
                switch (option) {
                    case "bin":
                        serialize = new BinaryAdapter();
                        break;
                    case "xml":
                        serialize = new XmlAdapter();
                        break;
                    default:
                        throw new FormatException("Неизвестный формат " + option);
                }
            }
        }

        private void LoadEmployeeList()
        {
            try
            {
                employees = serialize.Deserialize();
                currentId = employees.Count == 0 ? 0 : employees[employees.Count - 1].Id;
            } catch (SerializeException exception)
            {
                employees = new List<Employee>();
                currentId = 0;
            }  
        }

        public bool InsertEmployee(Employee employee)
        {
            currentId++;
            employee.Id = currentId;
            employees.Add(employee);
            return true;
        }

        public bool RemoveEmployeeById(int id)
        {
            Employee employee = GetEmployeeById(id);
            if (employee != null)
            {
                employees.Remove(employee);
                return true;
            }
            return false;
        }

        public Employee GetEmployeeById(int id)
        {
            foreach (var employee in employees)
            {
                if (employee.Id == id)
                {
                    return employee;
                }
            }
            return null;
        }

        public IList<Employee> GetEmployeesByName(string name)
        {
            IList<Employee> employeesByName = new List<Employee>();
            foreach (var employee in employees)
            {
                if (employee.Name == name)
                {
                    employeesByName.Add(employee);
                }
            }
            return employeesByName;
        }

        public IList<Employee> GetEmployeesByPosition(string position)
        {
            IList<Employee> employeesByPosition = new List<Employee>();

            foreach (var employee in employees)
            {
                if (employee.Position == position)
                {
                    employeesByPosition.Add(employee);
                }
            }
            return employeesByPosition;
        }

        public void SaveChanges()
        {
            serialize.Serialize(employees);
        }
    }
}
