using System.Windows.Forms;

namespace Ball
{
    public partial class Form1 : Form
    {
        static Bitmap bmp;
        static Graphics g;

        public List<Ball> Balls;
        public Ball newBall;
        public int numballs;

        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(PCT_CANVAS.Width, PCT_CANVAS.Height);
            g = Graphics.FromImage(bmp);
            PCT_CANVAS.Image = bmp;
            timer1.Stop();
            Balls = new List<Ball>();
        }

        private void startGame_Click(object sender, EventArgs e)
        {
            numballs = Int32.Parse(textBox1.Text);
            for(int i = 0; i < numballs; i++)
            {
                newBall = new Ball();
                Balls.Add(newBall);
            }
            timer1.Start();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            g.Clear(Color.Black);

            for (int i = 0; i < numballs; i++)
            {

                Balls.ElementAt(i).posXBall += Balls.ElementAt(i).velx;
                Balls.ElementAt(i).posYBall += Balls.ElementAt(i).vely;

                // Check for collisions with walls
                if (Balls.ElementAt(i).posXBall < 0)
                {
                    Balls.ElementAt(i).posXBall = 0;
                    Balls.ElementAt(i).velx = -Balls.ElementAt(i).velx;
                }
                else if (Balls.ElementAt(i).posXBall + Balls.ElementAt(i).sizeBall > PCT_CANVAS.Width)
                {
                    Balls.ElementAt(i).posXBall = PCT_CANVAS.Width - Balls.ElementAt(i).sizeBall;
                    Balls.ElementAt(i).velx = -Balls.ElementAt(i).velx;
                }
                if (Balls.ElementAt(i).posYBall < 0)
                {
                    Balls.ElementAt(i).posYBall = 0;
                    Balls.ElementAt(i).vely = -Balls.ElementAt(i).vely;
                }
                else if (Balls.ElementAt(i).posYBall + Balls.ElementAt(i).sizeBall > PCT_CANVAS.Height)
                {
                    Balls.ElementAt(i).posYBall = PCT_CANVAS.Height - Balls.ElementAt(i).sizeBall;
                    Balls.ElementAt(i).vely = -Balls.ElementAt(i).vely;
                }

                    // Check for collisions with other balls
                    for (int j = i + 1; j < numballs; j++)
                    {
                        float dx = Balls.ElementAt(i).posXBall - Balls.ElementAt(j).posXBall;
                        float dy = Balls.ElementAt(i).posYBall - Balls.ElementAt(j).posYBall;
                        float dist = (float)Math.Sqrt(dx * dx + dy * dy);

                        if (dist < Balls.ElementAt(i).sizeBall*0.60+ Balls.ElementAt(j).sizeBall*0.60)
                        {
                            // Collision detected! Invert velocities of both balls
                            float angle = (float)Math.Atan2(dy, dx);
                            float sin = (float)Math.Sin(angle);
                            float cos = (float)Math.Cos(angle);

                            // Rotate ball velocities
                            float velXBall1 = Balls.ElementAt(i).velx * cos + Balls.ElementAt(i).vely * sin;
                            float velYBall1 = Balls.ElementAt(i).vely * cos - Balls.ElementAt(i).velx * sin;
                            float velXBall2 = Balls.ElementAt(j).velx * cos + Balls.ElementAt(j).vely * sin;
                            float velYBall2 = Balls.ElementAt(j).vely * cos - Balls.ElementAt(j).velx * sin;

                            // Update ball velocities
                            float finalVelXBall1 = ((Balls.ElementAt(i).sizeBall - Balls.ElementAt(j).sizeBall) * velXBall1 + 2f * Balls.ElementAt(j).sizeBall * velXBall2) / (Balls.ElementAt(i).sizeBall + Balls.ElementAt(j).sizeBall);
                            float finalVelXBall2 = ((Balls.ElementAt(j).sizeBall - Balls.ElementAt(i).sizeBall) * velXBall2 + 2f * Balls.ElementAt(i).sizeBall * velXBall1) / (Balls.ElementAt(i).sizeBall + Balls.ElementAt(j).sizeBall);
                            Balls.ElementAt(i).velx = finalVelXBall1 * cos - velYBall1 * sin;
                            Balls.ElementAt(i).vely = velYBall1 * cos + finalVelXBall1 * sin;
                            Balls.ElementAt(j).velx = finalVelXBall2 * cos - velYBall2 * sin;
                            Balls.ElementAt(j).vely = velYBall2 * cos + finalVelXBall2 * sin;

                            // Move balls apart so they're not overlapping
                            float overlap = 0.4f * (Balls.ElementAt(i).sizeBall + Balls.ElementAt(j).sizeBall - dist + 1f);
                            Balls.ElementAt(i).posXBall += overlap * cos;
                            Balls.ElementAt(i).posYBall += overlap * sin;
                            Balls.ElementAt(j).posXBall -= overlap * cos;
                            Balls.ElementAt(j).posYBall -= overlap * sin;
                        }
                    }
            }

            drawBalls();
        }

        private void drawBalls()
        {

            for (int i = 0; i < numballs; i++)
            {
                g.FillEllipse(new SolidBrush(Balls.ElementAt(i).colorB), Balls.ElementAt(i).posXBall, Balls.ElementAt(i).posYBall, Balls.ElementAt(i).sizeBall, Balls.ElementAt(i).sizeBall);
                g.DrawEllipse(Pens.Black,Balls.ElementAt(i).posXBall, Balls.ElementAt(i).posYBall, Balls.ElementAt(i).sizeBall, Balls.ElementAt(i).sizeBall);
            }
            PCT_CANVAS.Invalidate();
        }
    }
}