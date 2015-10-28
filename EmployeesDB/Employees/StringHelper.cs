using System;

namespace EmployeesDB.Employees
{
    public static class StringHelper
    {
        public static string Addjust(this string str, int cellSize)
        {
            return str.Length >= cellSize ? str : str + new String(' ', cellSize - str.Length);
        }
    }
}
