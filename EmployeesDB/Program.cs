using System;
using System.IO;
using EmployeesDB.DB;
using EmployeesDB.Menu;

namespace EmployeesDB
{
    class Program
    {
        static void Main( string[] args )
        {
            try
            {
                EmployeeDB db = new EmployeeDB();

                new MainMenu(db).Initialize();

                db.SaveChanges();

            } catch (FormatException exception)
            {
                Console.WriteLine(exception.Message);
            } catch (FileNotFoundException exception)
            {
                Console.WriteLine("Файл options.ini отсутствует.");
            }                     
        }
    }
}
