using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_2
{
    class FixWorker: Worker, IComparable
    {
        public FixWorker(int Salary) : base(Salary)
        {
        }
        protected override double CalculateAvgSalary(int Salary)
        {
            return Salary;
        }
        public int CompareTo(object o)
        {
            Worker p = o as Worker;
            if (p?.Salary < this.Salary) return -1;
            else if (p?.Salary > this.Salary) return 1;
            else return 0;
        }
    }
}
