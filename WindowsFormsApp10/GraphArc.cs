using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WindowsFormsApp10
{
    class GraphArc : GraphLine
    {
        private Pen blackpen;
        private Pen redpen;
        public GraphArc(int x1, int y1, int x2, int y2, bool loop, int radius, int number) : base(x1, y1, x2, y2, radius, loop, number)
        {
            blackpen = new Pen(Color.Black, 2);
            blackpen.CustomEndCap = new AdjustableArrowCap(6, 6);
            redpen = new Pen(Color.Red, 2);
            redpen.CustomEndCap = new AdjustableArrowCap(6, 6);
        }
        public override Graphics Draw(Graphics graf)
        {
            if (IsLoop)
            {
                graf.DrawCurve(blackpen, points);
                return graf;
            }
            else
            {
                graf.DrawLine(blackpen, points[0].X, points[0].Y, points[1].X, points[1].Y);
                return graf;
            }
        }
        public override Graphics DrawHighlight(Graphics graf)
        {
            if (IsLoop)
            {
                graf.DrawCurve(redpen, points);
                return graf;
            }
            else
            {
                graf.DrawLine(redpen, points[0].X, points[0].Y, points[1].X, points[1].Y);
                return graf;
            }
        }
    }
}
