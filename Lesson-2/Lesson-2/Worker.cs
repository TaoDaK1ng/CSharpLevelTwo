using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_2
{
    abstract class Worker
    {
        public int Salary;
        //public int WorkTime {get; set;}
        public Worker(int Salary)
        {
            this.Salary = Salary;
        }
        protected abstract double CalculateAvgSalary(int Salary);
    }
}
