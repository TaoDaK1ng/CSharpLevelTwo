using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lesson_1_Chagalysov
{
    class Ship: BaseObject
    {
        private int _energy = 100;
        private int _score = 0;
        public int Energy { get { return _energy; } set { _energy = value; } }
        public int Score => _score;
        public static event Message MessageDie;
        public static event Message MessageWin;
        public static event EventHandler OnDie;
        public static event EventHandler OnFire;
        public static event EventHandler OnCollision;
        Image image;
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            image = new Bitmap("ship.png");
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y);
        }
        public override void Update()
        {   
        }
        public void EnergyLow(int n) => _energy -= n;
        public void EnergyUp(int n) => _energy += n;
        public void ScoreUp(int n) => _score += n;
        public void Fire() => OnFire?.Invoke(this, EventArgs.Empty);
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y - 5;
        }
        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y + 5;
        }
        public void Collision() => OnCollision?.Invoke(this, EventArgs.Empty);
        public void Die()
        {
            MessageDie?.Invoke();
            OnDie?.Invoke(this, EventArgs.Empty);
        }
        public void Win() => MessageWin?.Invoke();
    }
}
