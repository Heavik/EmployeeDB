using System.Collections.Generic;
using EmployeesDB.Employees;

namespace EmployeesDB.Serialization
{
    public interface ISerialize
    {
        void Serialize( IList<Employee> employees );
        IList<Employee> Deserialize();
    }
}
