using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_5
{
    class Department
    {
        public string Name { get; set; }
        public string Employee { get; set; }
        public Department(string name)
        {
            Name = name;            
        }
        public Department(string name, string employee) : this(name)
        {
            Employee = employee;
        }
    }
}
