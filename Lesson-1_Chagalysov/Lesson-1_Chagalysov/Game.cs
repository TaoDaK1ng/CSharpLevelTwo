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
        private static Asteroid[] _asteroids;
        private static Meteorite[] _meteorites;
        private static Bullet bullet;
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
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            Timer timer = new Timer { Interval = 10 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (Star obj in _stars) obj.Draw();
            foreach (Asteroid obj in _asteroids) obj.Draw();
            foreach (Meteorite obj in _meteorites) obj.Draw();
            bullet.Draw();
            Buffer.Render();
        }
        public static void Load()
        {
            _stars = new Star[15];
            _asteroids = new Asteroid[5];
            _meteorites = new Meteorite[15];
            bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(5, 5));
            Random rnd = new Random();
            for (int i = 0; i < _stars.Length; i++)
                _stars[i] = new Star(new Point(Game.Width, rnd.Next(0, Game.Height)), new Point(rnd.Next(0, 30), 10), new Size(5, 5));
            for (int i = 0; i < _asteroids.Length; i++)
                _asteroids[i] = new Asteroid(new Point(Game.Width, rnd.Next(0, Game.Height)), new Point(rnd.Next(0, 20), 10), new Size(50, 50));
            for (int i = 0; i < _meteorites.Length; i++)
                _meteorites[i] = new Meteorite(new Point(Game.Width, rnd.Next(0, Game.Height)), new Point(rnd.Next(0, 20), 10), new Size(5, 5));
        }
        public static void Update()
        {
            foreach (Star obj in _stars) obj.Update();
            foreach (Asteroid obj in _asteroids)
            {
                obj.Update();
                if (obj.Collision(bullet))
                {
                    System.Media.SystemSounds.Beep.Play();
                }   
            } 
            foreach (Meteorite obj in _meteorites) obj.Update();
            bullet.Update();
        }
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
    }
}
