using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        public static int INITIAL_SNAKE_LENGTH = 30,
                            APPLE_DIAMETER = 16,
                            WALL_WIDTH = 25,
                            HALF_APPLE = APPLE_DIAMETER / 2,
                            WIDTH,
                            HEIGTH,
                            apple_eaten;

        public static Graphics g;

        Timer timer = new Timer();


        public Form1()
        {
            InitializeComponent();

            WIDTH = this.ClientSize.Width - WALL_WIDTH * 2;
            HEIGTH = this.ClientSize.Height - WALL_WIDTH * 2;

            timer.Tick += Timer_Tick;
            timer.Interval = 1;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Panel panel_phone = new Panel()
            {
                Parent = this,
                BackColor = Color.Yellow,
                Size = this.ClientSize,
            };

            g = panel_phone.CreateGraphics();

            Initialize();
        }

        private void Initialize()
        {
            g.FillRectangle(Brushes.Yellow, this.ClientRectangle);

            apple_eaten = 0;

            Snake.CreateSnake(INITIAL_SNAKE_LENGTH);
            Apple.GenerateCoord();

            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Coord next = NextSnakeHead();

            Snake.MoveSnake(next);
            IfCrashed(next);
            IfAppleEaten(next);

            Wall.DrawAll();
        }

        private Coord NextSnakeHead()
        {
            Coord head = Snake.GetHead();

            switch (Snake.GetDirection())
            {
                case DIRECTION.UP:
                    return new Coord(head.x, head.y - 1 == WALL_WIDTH - 1 ? HEIGTH + WALL_WIDTH - HALF_APPLE : head.y - 1);
                case DIRECTION.RIGHT:
                    return new Coord(head.x + 1 == WIDTH + WALL_WIDTH + 1 ? WALL_WIDTH - HALF_APPLE : head.x + 1, head.y);
                case DIRECTION.DOWN:
                    return new Coord(head.x, head.y + 1 == HEIGTH + WALL_WIDTH + 1 ? WALL_WIDTH - HALF_APPLE : head.y + 1);
                case DIRECTION.LEFT:
                    return new Coord(head.x - 1 == WALL_WIDTH - 1 ? WIDTH + WALL_WIDTH - HALF_APPLE : head.x - 1, head.y);
                default:
                    return new Coord(head.x, head.y);
            }
        }

        private void IfAppleEaten(Coord snake)
        {
            if (Coords.GetDistBetweenPoints(snake, Apple.GetCoord()) <= APPLE_DIAMETER)
            {
                Apple.Clear();
                Apple.GenerateCoord();
                Snake.Grow();

                apple_eaten++;
            }
        }

        private void IfCrashed(Coord snake)
        {
            int snake_len = Snake.GetLength();

            for (int i = 0; i < snake_len-30; i += 10)
            {
                if (Coords.GetDistBetweenPoints(snake, Snake.GetBodyCoord(i)) <= APPLE_DIAMETER)
                {
                    timer.Stop();
                    MessageBox.Show("Your score: " + apple_eaten);

                    Initialize();
                    this.ResumeLayout();

                }
            }
        }

        private void KeyHandling(object sender, KeyEventArgs e)
        {
            DIRECTION snake_dir = Snake.GetDirection();

            switch (e.KeyData)  //Нажатие стрелки
            {
                case Keys.Up:
                    {
                        if (snake_dir != DIRECTION.DOWN)
                        {
                            Snake.SetDirection(DIRECTION.UP);
                        }
                        break;
                    }
                case Keys.Right:
                    {
                        if (snake_dir != DIRECTION.LEFT)
                        {
                            Snake.SetDirection(DIRECTION.RIGHT);
                        }
                        break;
                    }
                case Keys.Down:
                    {
                        if (snake_dir != DIRECTION.UP)
                        {
                            Snake.SetDirection(DIRECTION.DOWN);
                        }
                        break;
                    }
                case Keys.Left:
                    {
                        if (snake_dir != DIRECTION.RIGHT)
                        {
                            Snake.SetDirection(DIRECTION.LEFT);
                        }
                        break;
                    }
            }
        }
    }
}
