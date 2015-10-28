using System;

namespace EmployeesDB.Serialization
{
    public class SerializeException : Exception
    {      
        public SerializeException(string message) : base(message)
        {
        }
    }
}
