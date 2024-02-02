using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp10
{
    class GraphEdge : GraphLine
    {

        public GraphEdge(int x1, int y1, int x2, int y2, bool loop, int radius, int number) : base(x1, y1, x2, y2, radius, loop, number)
        {

        }
        public override Graphics Draw(Graphics graf)
        {
            if (IsLoop)
            {
                graf.DrawCurve(new Pen(Color.Black, 2), points);
                return graf;
            }
            else
            {
                graf.DrawLine(new Pen(Color.Black, 2), points[0].X, points[0].Y, points[1].X, points[1].Y);
                return graf;
            }
        }
        public override Graphics DrawHighlight(Graphics graf)
        {
            if (IsLoop)
            {
                graf.DrawCurve(new Pen(Color.Red, 2), points);
                return graf;
            }
            else
            {
                graf.DrawLine(new Pen(Color.Red, 2), points[0].X, points[0].Y, points[1].X, points[1].Y);
                return graf;
            }
        }
    }
}
