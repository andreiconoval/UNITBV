using DemoEF.DataMapper;
using DemoEF.DomainModel;
using System;
using System.Linq;

namespace DemoEF
{
    class Program
    {
        static void Main(string[] args)
        {
            //listAllEmployees();

            //addEmployee();

            //modifyEmployee();

            //deleteEmployee();

            listAllEmployeesWithDetails();
        }

        private static void listAllEmployeesWithDetails()
        {
            using (var context = new Northwind())
            {
                //var allEmployees = context.Employees.Include("Territories").OrderBy(employee => employee.FirstName).ThenBy(employee => employee.LastName) ;
                var allEmployees = context.Employees.Include("Territories").Include("Territories.Region").OrderBy(employee => employee.FirstName).ThenBy(employee => employee.LastName);
                foreach (var item in allEmployees)
                {
                    Console.WriteLine($"{item.FirstName}, {item.LastName}, {item.Country}");
                    foreach (var territory in item.Territories)
                    {
                        Console.WriteLine($"\t Territory: {territory.TerritoryDescription.Trim()} from region {territory.Region.RegionDescription}");
                    }
                }
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Deletes the employee.
        /// </summary>
        private static void deleteEmployee()
        {
            using (var context = new Northwind())
            {
                try
                {
                    var thatEmployee = context.Employees.Where(emp => emp.FirstName == "Wilhelm" && emp.LastName == "Ionescu").SingleOrDefault();
                    if (thatEmployee != null)
                    {
                        context.Employees.Remove(thatEmployee);
                        context.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("No guy named W.I.");
                    }
                }
                catch (Exception exception) // e.g. because there are more than one guys names "Wilhelm Popescu"
                {
                    Console.WriteLine($"Exception caught: {exception.Message}");
                }
            }
        }

        /// <summary>
        /// Modifies the employee.
        /// </summary>
        private static void modifyEmployee()
        {
            using (var context = new Northwind())
            {
                try
                {
                    var thatEmployee = context.Employees.Where(emp => emp.FirstName == "Wilhelm" && emp.LastName == "Popescu").SingleOrDefault();
                    if (thatEmployee != null)
                    {
                        thatEmployee.LastName = "Ionescu";
                        context.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("No guy named W.P.");
                    }
                }
                catch(Exception exception) // e.g. because there are more than one guys names "Wilhelm Popescu"
                {
                    Console.WriteLine($"Exception caught: {exception.Message}");
                }
            }
        }

        /// <summary>
        /// Adds the employee.
        /// </summary>
        private static void addEmployee()
        {
            using (var context = new Northwind())
            {
                var employee = new Employee
                {
                    FirstName = "Wilhelm",
                    LastName = "Popescu",
                };
                context.Employees.Add(employee);
                context.SaveChanges();
                Console.WriteLine($"PK of newly added record: {employee.EmployeeID}");
            }
        }

        /// <summary>
        /// Lists all employees.
        /// </summary>
        private static void listAllEmployees()
        {
            using (var context = new Northwind())
            {
                var allEmployees = from employee in context.Employees
                                   orderby employee.FirstName
                                   select employee;

                foreach (var item in allEmployees)
                {
                    Console.WriteLine($"{item.FirstName} {item.LastName}");
                }
            }
        }
    }
}
