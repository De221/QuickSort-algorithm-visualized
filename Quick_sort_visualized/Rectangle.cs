using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Quick_sort_visualized
{
    public class Rectangle
    {
        private Point _location;

        private int _width;

        private Color _color;

        private int _number;

        public Rectangle(Point location, int width, Color color, int number)
        {
            _location = location;
            _width = width;
            _color = color;
            _number = number;
        }


        public Point location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = location;
            }
        }
        public int width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = width;
            }
        }
        public Color color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = color;
            }
        }
        public int number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = number;
            }
        }

        public void Paint(Graphics g)
        {
            int fontSize = 18;
            Size textSize = TextRenderer.MeasureText(_number.ToString(), new Font("Arial", fontSize));
            using (var brush = new SolidBrush(color))
                g.FillRectangle(brush, location.X, location.Y, _width, 25);
            using (var pen = new Pen(Color.FromKnownColor(KnownColor.Black), 1))
                g.DrawRectangle(pen, location.X, location.Y, _width, 25);

            while (textSize.Width > _width - 2)
            {
                fontSize--;
                textSize = TextRenderer.MeasureText(_number.ToString(), new Font("Arial", fontSize));
            }
            int center_y = (int)Math.Round((double)location.Y + 12.5 - (textSize.Height / 2), MidpointRounding.AwayFromZero);
            int center_x = (int)Math.Round((double)location.X + _width / 2 - (textSize.Width / 2), MidpointRounding.AwayFromZero);
            using (var brush = new SolidBrush(Color.Black))
                g.DrawString(_number.ToString(), new Font("Arial", fontSize), brush, new Point(center_x, center_y));
        }
    }
}
