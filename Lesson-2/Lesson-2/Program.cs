using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Worker[] workers = new Worker[10];
            for (int i = 0; i < 5; i++)
            {
                workers[i] = new HourWorker(100);
            }
            for (int i = 5; i < 10; i++)
            {
                workers[i] = new FixWorker(25000);
            }            
        }
    }
}
