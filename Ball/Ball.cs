using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ball
{
    public class Ball
    {
        public float sizeBall;
        public float posXBall;
        public float posYBall;
        public float velx, vely;
        public Color colorB;
        public Ball()
        {
            Random r = new Random();
            sizeBall = r.Next(20, 50);
            posXBall = r.Next(0, 600);
            posYBall = r.Next(0, 250);
            velx = r.Next(4, 15);
            vely = r.Next(4, 15);
            colorB = Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));
        }
    }
}
