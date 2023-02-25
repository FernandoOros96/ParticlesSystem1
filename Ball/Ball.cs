using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ball
{
    public class Ball
    {
        public float sizeBallx;
        public float sizeBally;
        public float posXBall;
        public float posYBall;
        public float velx, vely;
        public Color colorB;
        public int timeLive;
        public Ball(Size size)
        {
            Random r = new Random();
            sizeBallx = r.Next(2, 4);
            sizeBally = r.Next(20, 60);
            posXBall = r.Next(0, size.Width);
            posYBall = r.Next(-size.Height, 0);
            velx = 0;
            vely = r.Next(10, 50);
            colorB = Color.FromArgb(r.Next(256),0, 0, r.Next(256));
            timeLive = r.Next(20, 80);
        }
    }
}
