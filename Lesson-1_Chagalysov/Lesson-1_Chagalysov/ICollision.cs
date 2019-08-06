using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lesson_1_Chagalysov
{
    interface ICollision
    {
        bool Collision(ICollision o);
        Rectangle rect { get; }
    }
}
