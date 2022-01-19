using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Snake.Form1;

namespace Snake
{
    public static class Apple
    {
        private static Coord _coord_apple;
        private static Brush _brush_apple = Brushes.Red,
                                _brush_back = Brushes.Yellow;

        public static void GenerateCoord()
        {
        lbl: _coord_apple = Coords.GetCoord();

            int _snake_len = Snake.GetLength();

            for (int i = 0; i < _snake_len; i += 10)
            {
                if (Coords.GetDistBetweenPoints(_coord_apple, Snake.GetBodyCoord(i)) <= APPLE_DIAMETER)
                {
                    goto lbl;
                }
            }

            g.FillEllipse(
                            _brush_apple,
                            _coord_apple.x - APPLE_DIAMETER / 2,
                            _coord_apple.y - APPLE_DIAMETER / 2,
                            APPLE_DIAMETER,
                            APPLE_DIAMETER
                );
        }

        static public void Clear()
        {
            g.FillEllipse(
                            _brush_back,
                            _coord_apple.x - APPLE_DIAMETER / 2,
                            _coord_apple.y - APPLE_DIAMETER / 2,
                            APPLE_DIAMETER,
                            APPLE_DIAMETER
                );
        }

        public static Coord GetCoord()
        {
            return _coord_apple;
        }
    }
}
