using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_5
{
   public class Repository
    {
        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<Department> Departments { get; set; }
        static Random rnd = new Random();
        public Repository(int EmployeesCount, int DepartmentsCount)
        {
            //Employees = new ObservableCollection<Employee>();
            //Departments = new ObservableCollection<Department>();
            //for (int i = 0; i < EmployeesCount; i++)
            //{
            //    Employees.Add(new Employee("Имя", "Фамилия", "Отчество", i + 1, rnd.Next(DepartmentsCount)));
            //}
            //for (int i = 0; i < DepartmentsCount; i++)
            //{
            //    Departments.Add(new Department("Департамент", i + 1));
            //}
        }
        public void DeleteDepartment(int index)
        {
            //for (int i = 0; i < Departments.Count; i++)
            //{
            //    if (Departments[i].DepartmentID == index)
            //    {
            //        Departments.RemoveAt(i);
            //    }
            //}
            //for (int i = 0; i < Employees.Count; i++)
            //{
            //    if (Employees[i].DepartmentId == index)
            //    {
            //        Employees.RemoveAt(i);
            //    }
            //}
        }

    }
}
