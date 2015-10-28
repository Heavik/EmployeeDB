using System;

namespace EmployeesDB.Employees
{
    [Serializable]
    public class Employee
    {
        private int? age;
        private decimal salary;

        public int Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }

        public int? Age {
            get { return age; }
            set
            {
                if (value > 0 && value < 100)
                {
                    age = value;
                } else
                {
                    age = null;
                }
            }
        }

        public decimal Salary
        {
            get { return salary; }
            set
            {
                if (value > 0)
                {
                    salary = value;
                } 
            }
        }

        public Employee() { }

        public Employee(int id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return String.Format("{0}   |{1}|{2}|{3}\t|{4}|{5}",
                Id,
                Name.Addjust(11),
                Surname.Addjust(15),
                Age,
                Position.Addjust(23),
                Salary);
        }
    }
}
