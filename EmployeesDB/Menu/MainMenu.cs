using System;
using System.Collections.Generic;
using EmployeesDB.DB;
using EmployeesDB.Employees;

namespace EmployeesDB.Menu
{
    public class MainMenu : ConsoleMenu
    {
        private IEmployeeDB db;

        public MainMenu(IEmployeeDB db)
        {
            this.db = db;
        }

        public override void Initialize()
        {
           ClearMemu(); 
           AddItem("Добавить сотрудника", AddNewEmployee);
           AddItem("Удалить сотрудника", RemoveEmployee);
           AddItem("Показать всех сотрудников", ShowAllEmployees);
           AddItem("Найти сотрудника по id", FindEmployeeById);
           AddItem("Найти сотрудников по имени", FindEmployeesByName);
           AddItem("Найти сотрудников по должности", FindEmployeesByPostion);
           AddItem("Выход", Exit);

           DrawMenu();
           ListenKey();
        }

        private void AddNewEmployee(object sender, EventArgs args)
        {
            Employee employee = AddEmployeeDialog();
            if (employee != null)
            {
                db.InsertEmployee(employee);
            }
            Initialize();
        }

        private Employee AddEmployeeDialog()
        {
           int age;
           decimal salary;
           Console.Clear();
           Console.Write("Введите имя сотрудника: ");
           string name = Console.ReadLine();
            if (String.IsNullOrEmpty(name))
            {
                ShowMessage("Имя не может быть пустым");
                return null;
            }
           Console.Write("Введите фамилию сотрудника: ");
           string surname = Console.ReadLine();
           Console.Write("Введите возраст сотрудника: ");           
           Int32.TryParse(Console.ReadLine(), out age);
           Console.Write("Введите должность сотрудника: ");
           string position = Console.ReadLine();
           Console.Write("Введите зарплату сотрудника: ");
           Decimal.TryParse(Console.ReadLine(), out salary);

           return new Employee()
           {
               Name = name,
               Surname = surname,
               Age = age,
               Position = position,
               Salary = salary
           };
        }

        private void RemoveEmployee(object sender, EventArgs args)
        {
            Console.Clear();
            int id;
            Console.Write("Введите id сотрудника: ");
            if (Int32.TryParse(Console.ReadLine(), out id))
            {
                if (db.RemoveEmployeeById(id))
                {
                    ShowMessage("Пользователь с id " + id + " успешно удален");
                } else
                {
                    ShowMessage("Пользователя с таким id не существует");
                }
            } else
            {
                ShowMessage("Неверный формат id");
            }
            Initialize();
        }

        private void ShowAllEmployees(object sender, EventArgs args)
        {
            Console.Clear();
            ShowEmployess(db.Employees);
            Initialize();
        }

        private void FindEmployeeById(object sender, EventArgs args)
        {
            Console.Clear();
            int id;
            Console.Write("Введите id сотрудника: ");
            if (Int32.TryParse(Console.ReadLine(), out id))
            {
                Employee employee = db.GetEmployeeById(id);
                if (employee != null)
                {
                    ShowHeader();
                    Console.WriteLine(employee);
                    Console.WriteLine("Нажмите ENTER для продолжения...");
                    Console.ReadLine();
                } else
                {
                    ShowMessage("Сотрудника с id " + id + " не существует");
                }
            } else
            {
                ShowMessage("Неверный формат id");
            }
            Initialize();
        }

        private void FindEmployeesByName(object sender, EventArgs args)
        {
            Console.Clear();
            Console.Write("Введите имя сотрудника: ");
            string name = Console.ReadLine();
            var employees = db.GetEmployeesByName(name);
            ShowEmployess(employees);
            Initialize();
        }

        private void FindEmployeesByPostion(object sender, EventArgs args)
        {
            Console.Clear();
            Console.Write("Введите должность: ");
            string postion = Console.ReadLine();
            var employees = db.GetEmployeesByPosition(postion);
            ShowEmployess(employees);

            Initialize();
        }

        private void ShowEmployess(IList<Employee> employees)
        {
            Console.WriteLine();
            if (employees.Count == 0)
            {
                ShowMessage("Список пуст или не удовлетворяет заданным критериям.");
            } else
            {
                ShowHeader();
                foreach (var employee in employees)
                {
                    Console.WriteLine(employee);
                }
                ShowMessage(String.Empty);    
            }           
        }

        private void Exit(object sender, EventArgs args)
        {
            CloseMenu = true;
        }

        private void ShowHeader()
        {
            Console.WriteLine("ID  |Имя\t|Фамилия\t|Возраст|Должность\t\t|Зарплата");
            Console.WriteLine(new String('-', 75));    
        }

        private void ShowMessage(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Нажмите ENTER для продолжения...");
            Console.ReadLine();
        }
    }
}
