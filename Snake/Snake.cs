using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Snake.Form1;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    public static class Snake
    {
        private static CoordDir[] _body;

        private static DIRECTION _dir;

        private static int _current_length,
                             _last_index,
                             _grow = 0;

        private static Brush _brush_snake = Brushes.Green,
                                _brush_back = Brushes.Yellow;

        public static void CreateSnake(int initial_length)
        {
            _body = new CoordDir[4000];
            _dir = DIRECTION.RIGHT;
            _last_index = 0;
            _current_length = initial_length;

            _body[_last_index + initial_length - 1] = new CoordDir(new Coord(400, 150), _dir);
        }

        private static void DrawSnake(int ind)
        {
            float f1, f2;

            switch (_body[_last_index].dir)
            {
                case DIRECTION.UP:
                    f1 = 0f;
                    f2 = 180f;
                    break;
                case DIRECTION.RIGHT:
                    f1 = 90f;
                    f2 = 180f;
                    break;
                case DIRECTION.DOWN:
                    f1 = 180f;
                    f2 = 180f;
                    break;
                case DIRECTION.LEFT:
                    f1 = 270f;
                    f2 = 180f;
                    break;
                default:
                    f1 = 0f;
                    f2 = 0f;
                    break;
            }

            g.FillEllipse(
                            _brush_snake,
                            _body[ind].coord.x - HALF_APPLE,
                            _body[ind].coord.y - HALF_APPLE,
                            APPLE_DIAMETER,
                            APPLE_DIAMETER
                );

            g.DrawPie(
                            new Pen(_brush_back),
                            _body[_last_index].coord.x - HALF_APPLE - 1,
                            _body[_last_index].coord.y - HALF_APPLE - 1,
                            APPLE_DIAMETER + 2,
                            APPLE_DIAMETER + 2,
                            f1,
                            f2
                );
        }

        public static void MoveSnake(Coord next)
        {
            if (_grow == 0)
            {
                _last_index = (_last_index + 1) % _body.Length;
            }
            else
            {
                _current_length++;
                _grow--;
            }

            int head = (_last_index + _current_length - 1) % _body.Length;

            _body[head] = new CoordDir(next, _dir);

            DrawSnake(head);
        }

        public static void Grow()
        {
            _grow = 10;
        }

        public static Coord GetHead()
        {
            return GetBodyCoord(_current_length - 1);
        }

        public static int GetLength()
        {
            return _current_length;
        }

        public static int GetLastIndex()
        {
            return _last_index;
        }

        public static DIRECTION GetDirection()
        {
            return _dir;
        }

        public static void SetDirection(DIRECTION new_dir)
        {
            _dir = new_dir;
        }

        public static Coord GetBodyCoord(int num)
        {
            return _body[(_last_index + num) % _body.Length].coord;
        }
    }

    public struct CoordDir
    {
        public Coord coord;
        public DIRECTION dir;

        public CoordDir(Coord c, DIRECTION d)
        {
            coord = c;
            dir = d;
        }
    }

    public enum DIRECTION
    {
        UP,
        RIGHT,
        DOWN,
        LEFT
    }
}
