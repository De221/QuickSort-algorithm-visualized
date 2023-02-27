using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick_sort_visualized
{
    public class SwapArc
    {
        private Point _location1;

        private Point _location2;

        public SwapArc(Point loc1, Point loc2)
        {
            _location1 = loc1;
            _location2 = loc2;
        }

        public Point location1
        {
            get
            {
                return _location1;
            }
            set
            {
                _location1 = location1;
            }
        }

        public Point location2
        {
            get
            {
                return _location2;
            }
            set
            {
                _location2 = location2;
            }
        }    

        public void Paint(Graphics g)
        {
            Color color = Color.Black;
            using (var pen = new Pen(color, 3))
            {
                g.DrawLine(pen, _location1.X, _location1.Y - 20, _location2.X, _location2.Y - 20);
                g.DrawLine(pen, _location1.X, _location1.Y, _location1.X, _location1.Y - 20);
                g.DrawLine(pen, _location2.X, _location2.Y, _location2.X, _location2.Y - 20);
            }
        }
    }
}
