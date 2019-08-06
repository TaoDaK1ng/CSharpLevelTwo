using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_2
{
    class HourWorker : Worker
    {
       public HourWorker(int Salary) : base(Salary)
        {
        }
       protected override double CalculateAvgSalary(int Salary)
        {
            return 20.8 * 8 * Salary;
        }
    }
}
