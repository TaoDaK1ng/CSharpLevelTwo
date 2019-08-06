using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Lesson_1_Chagalysov
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        private static Star[] _stars;
        private static List<Asteroid> _asteroids;
        private static Meteorite[] _meteorites;
        private static Aidkit[] _aidkits;
        private static Bullet _bullet;
        private static Ship _ship;
        private static Timer _timer;
        private static Random _rnd;
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }
        static Game()
        {
        }
        public static void Init(Form form)
        {
            // Графическое устройство для вывода графики            
            Graphics g;
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы
            try
            {
                Width = form.ClientSize.Width;
                Height = form.ClientSize.Height;
            }
            catch (ArgumentOutOfRangeException e) when (Width > 1000 || Height > 1000 || Width < 0 || Height > 0)
            {
                Console.WriteLine(e.Message);
            }
            form.KeyDown += Form_KeyDown;
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            _timer = new Timer { Interval = 10 };
            _timer.Start();
            _timer.Tick += Timer_Tick;
            Ship.MessageDie += Finish;
            Ship.MessageWin += Winner;
            Ship.OnFire += Ship_Fire;
            Ship.OnDie += (sender, args) => Console.WriteLine("Корабль уничтожен");
            Ship.OnCollision += (sender, args) => Console.WriteLine("Корабль столкнулся с астеройдом");
        }
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (Star obj in _stars) obj?.Draw();
            foreach (Asteroid obj in _asteroids) obj?.Draw();
            foreach (Meteorite obj in _meteorites) obj?.Draw();
            foreach (Aidkit obj in _aidkits) obj?.Draw();
            _bullet?.Draw();
            _ship?.Draw();
            if (_ship != null)
            Buffer.Graphics.DrawString("Energy:" + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
            Buffer.Graphics.DrawString("Score:" + _ship.Score, SystemFonts.DefaultFont, Brushes.White, 0, 15);
            Buffer.Render();
        }
        public static void Load()
        {

            _stars = new Star[15];
            _asteroids = new List<Asteroid>();
            _meteorites = new Meteorite[15];
            _aidkits = new Aidkit[4];
            _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(112, 83));
            _rnd = new Random();
            for (int i = 0; i < _stars.Length; i++)
                _stars[i] = new Star(new Point(Game.Width, _rnd.Next(0, Game.Height)), new Point(_rnd.Next(0, 30), 10), new Size(5, 5));
            for (int i = 0; i < Asteroid.Count; i++)
                _asteroids.Add(new Asteroid(new Point(Game.Width, _rnd.Next(0, Game.Height)), new Point(_rnd.Next(0, 20), 10), new Size(50, 50)));           
            for (int i = 0; i < _meteorites.Length; i++)
                _meteorites[i] = new Meteorite(new Point(Game.Width, _rnd.Next(0, Game.Height)), new Point(_rnd.Next(0, 20), 10), new Size(5, 5));
            for (int i = 0; i < _aidkits.Length; i++)
                _aidkits[i] = new Aidkit(new Point(Game.Width, _rnd.Next(0, Game.Height)), new Point(_rnd.Next(0, 20), 10), new Size(5, 5));
        }
        public static void Update()
        {            
            foreach (Star obj in _stars) obj.Update();
            foreach (Meteorite obj in _meteorites) obj.Update();
            for (var i = 0; i < _asteroids.Count; i++)
            {
                if (_asteroids[i] == null) continue;
                _asteroids[i].Update();
                if (_bullet != null && _bullet.Collision(_asteroids[i]))
                {
                    _ship?.ScoreUp(1);
                    System.Media.SystemSounds.Hand.Play();
                    _asteroids.Remove(_asteroids[i]);
                    _bullet = null;
                    continue;
                }
                if (!_ship.Collision(_asteroids[i])) continue;
                    _ship?.EnergyLow(_rnd.Next(1, 10));
                    _ship?.Collision();
                    System.Media.SystemSounds.Asterisk.Play();
            }
            if (_asteroids.Count == 0)
            {
                Asteroid.Count++;
                for (int i = 0; i < Asteroid.Count; i++)
                    _asteroids.Add(new Asteroid(new Point(Game.Width, _rnd.Next(0, Game.Height)), new Point(_rnd.Next(0, 20), 10), new Size(50, 50)));
            }
            if (_ship.Energy <= 0) _ship?.Die();
            if (_ship.Score == 20) _ship?.Win();
            for (int i = 0; i < _aidkits.Length; i++)
            {
                if (_aidkits[i] == null) continue;
                _aidkits[i].Update();
                if (_aidkits[i] != null && !_ship.Collision(_aidkits[i])) continue;
                System.Media.SystemSounds.Exclamation.Play();
                _ship?.EnergyUp(_rnd.Next(1, 10));
                _aidkits[i] = null; 
            }
            if (_ship.Energy >= 100) _ship.Energy = 100;
            _bullet?.Update();
            _ship.Update();
        }
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) _ship.Fire();
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
        }
        public static void Finish()
        {
            _timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, (Game.Width / 2) - 180, Game.Height / 2 - 50);
            Buffer.Render();
        }
        public static void Winner()
        {
            _timer.Stop();
            Buffer.Graphics.DrawString("You win", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, (Game.Width / 2) - 180, Game.Height / 2 - 50);
            Buffer.Render();
        }
        private static void Ship_Fire(object sender, EventArgs args)
        {
            _bullet = new Bullet(new Point(_ship.rect.X + 100, _ship.rect.Y + 40), new Point(2, 0), new Size(30, 6));
            Console.WriteLine("Выстрел");
        }
    }
}
