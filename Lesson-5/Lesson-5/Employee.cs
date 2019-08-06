using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_5
{
    class Employee
    {
        public string Name { get; set; }
        public string Department { get; set; }
        ObservableCollection<Employee> list = new ObservableCollection<Employee>();
        public Employee(string name, string department)
        {
            Name = name;
            Department = department;
        }
        public void AddEmployee(Employee employee)
        {
            list.Add(employee);
        }
    }
}
