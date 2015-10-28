using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using EmployeesDB.Employees;

namespace EmployeesDB.Serialization
{
    public class BinaryAdapter : ISerialize
    {
        private BinaryFormatter formatter = new BinaryFormatter();

        private const string FilePath = "employess.dat";

        public void Serialize(IList<Employee> employees)
        {
            using (var stream = new FileStream(FilePath, FileMode.OpenOrCreate))
            {
                formatter.Serialize(stream, employees);
            }
        }

        public IList<Employee> Deserialize()
        {
            IList<Employee> employees;
            try
            {
                using (var stream = new FileStream(FilePath, FileMode.OpenOrCreate))
                {
                    employees = formatter.Deserialize(stream) as IList<Employee>;
                }
            } catch (SerializationException exception)
            {
                throw new SerializeException(exception.Message);
            }
            
            return employees;
        }
    }
}
