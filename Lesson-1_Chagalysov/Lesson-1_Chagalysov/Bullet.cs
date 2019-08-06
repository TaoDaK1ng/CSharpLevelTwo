using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lesson_1_Chagalysov
{
    class Bullet : BaseObject
    {
        Image image;
        public int Power { get; set; }
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            image = new Bitmap("bullet2.png");
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y);
        }
        public override void Update()
        {
            Pos.X = Pos.X + 10;
        }
    }
}
