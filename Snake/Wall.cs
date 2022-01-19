using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Snake.Form1;

namespace Snake
{
    public static class Wall
    {
        private static Brush _brush = Brushes.Violet;
        private static int _width = Form1.ActiveForm.ClientSize.Width,
                            _heigth = Form1.ActiveForm.ClientSize.Height;


        public static void DrawAll()
        {
            g.FillRectangle(_brush, 0, 0, WALL_WIDTH, _heigth);
            g.FillRectangle(_brush, _width - WALL_WIDTH, 0, WALL_WIDTH, _heigth);
            g.FillRectangle(_brush, 0, 0, _width, WALL_WIDTH);
            g.FillRectangle(_brush, 0, _heigth - WALL_WIDTH, _width, WALL_WIDTH);
        }
    }
}
