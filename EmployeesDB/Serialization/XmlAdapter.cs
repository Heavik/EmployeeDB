using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using EmployeesDB.Employees;

namespace EmployeesDB.Serialization
{
    public class XmlAdapter : ISerialize
    {
        private XmlSerializer serializer = new XmlSerializer(typeof(Employee[]));

        private const string FilePath = "employees.xml";

        public void Serialize(IList<Employee> employees)
        {
            using (var stream = new FileStream(FilePath, FileMode.Truncate))
            {    
                serializer.Serialize(stream, employees.ToArray());
            }
        }

        public IList<Employee> Deserialize()
        {
            IList<Employee> employees = null;
            try
            {
                using (var stream = new FileStream(FilePath, FileMode.OpenOrCreate))
                {

                    Employee[] empl = serializer.Deserialize(stream) as Employee[];
                    if (empl != null)
                    {
                        employees = empl.ToList();
                    }
                }
            } catch (InvalidOperationException exception)
            {
                throw new SerializeException(exception.Message);
            }
            return employees;
        }
    }
}
