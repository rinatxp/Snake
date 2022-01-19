using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Snake.Form1;

namespace Snake
{
    public static class Coords
    {
        private static Random _random = new Random();

        public static Coord GetCoord()
        {
            int rand_x = _random.Next(WIDTH),
                rand_y = _random.Next(HEIGTH);

            return new Coord(rand_x + WALL_WIDTH + HALF_APPLE, rand_y + WALL_WIDTH + HALF_APPLE);
        }

        public static double GetDistBetweenPoints(Coord c1, Coord c2)
        {
            return Math.Sqrt(Math.Pow((c1.x - c2.x), 2) + Math.Pow((c1.y - c2.y), 2));
        }
    }

    public struct Coord
    {
        public int x;
        public int y;

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
