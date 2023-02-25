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
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(PCT_CANVAS.Width, PCT_CANVAS.Height);
            g = Graphics.FromImage(bmp);
            PCT_CANVAS.Image = bmp;
            timer1.Stop();
            numballs = 150;
            Balls = new List<Ball>();
            createBalls();
        }

        private void createBalls()
        {
            for(int i = 0; i < numballs; i++)
            {
                newBall = new Ball(PCT_CANVAS.Size);
                Balls.Add(newBall);
            }
            timer1.Start();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            g.Clear(Color.Black);

            for (int i = 0; i < numballs; i++)
            {

                Balls.ElementAt(i).posYBall += Balls.ElementAt(i).vely;

                Balls.ElementAt(i).timeLive--;

                if (Balls.ElementAt(i).timeLive == 0)
                {
                    Balls.ElementAt(i).posYBall = random.Next(-PCT_CANVAS.Height, 0);
                    Balls.ElementAt(i).vely = random.Next(12, 30);
                    Balls.ElementAt(i).timeLive = random.Next(30,50);
                }
                }

            drawBalls();
        }

        private void drawBalls()
        {

            for (int i = 0; i < numballs; i++)
            {
                g.FillEllipse(new SolidBrush(Balls.ElementAt(i).colorB), Balls.ElementAt(i).posXBall, Balls.ElementAt(i).posYBall, Balls.ElementAt(i).sizeBallx, Balls.ElementAt(i).sizeBally);
            }
            PCT_CANVAS.Invalidate();
        }
    }
}