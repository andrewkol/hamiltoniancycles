using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp10
{
    class GraphLine
    {
        protected int FirstVertexX, FistVertexY, SecondVertexX, SecondVertexY, RadiusOfVertex, CurrentNumber;
        protected bool IsLoop;
        protected Point[] points;
        private int dx, dy, dx1, dy1, S;
        private double h;

        public GraphLine(int x1, int y1, int x2, int y2, int radius, bool loop, int number)
        {
            this.FirstVertexX = x1;
            this.FistVertexY = y1;
            this.SecondVertexX = x2;
            this.SecondVertexY = y2;
            this.IsLoop = loop;
            this.RadiusOfVertex = radius;
            this.CurrentNumber = number;
            if (loop)
            {
                points = new Point[3];
                points[0].X = (int)(RadiusOfVertex * Math.Cos(2 * Math.PI)) + FirstVertexX;
                points[0].Y = (int)(RadiusOfVertex * Math.Sin(2 * Math.PI)) + FistVertexY;
                points[1].X = -(int)(RadiusOfVertex * Math.Cos(Math.PI / 2)) + FirstVertexX;
                points[1].Y = -(int)(RadiusOfVertex * Math.Sin(Math.PI / 2)) + FistVertexY + 40;
                points[2].X = (int)(RadiusOfVertex * Math.Cos(Math.PI)) + FirstVertexX;
                points[2].Y = (int)(RadiusOfVertex * Math.Sin(Math.PI)) + FistVertexY;
            }
            else
            {
                points = new Point[2];
                int moduleRaznX = Math.Abs(x2 - x1);
                int moduleRaznY = Math.Abs(y2 - y1);
                if (moduleRaznX >= moduleRaznY)
                {
                    if (SecondVertexX >= FirstVertexX)
                    {
                        points[0].X = (int)(RadiusOfVertex * Math.Cos(2 * Math.PI)) + x1;
                        points[0].Y = (int)(RadiusOfVertex * Math.Sin(2 * Math.PI)) + y1;
                        points[1].X = (int)(RadiusOfVertex * Math.Cos(Math.PI)) + x2;
                        points[1].Y = (int)(RadiusOfVertex * Math.Sin(Math.PI)) + y2;
                    }
                    else
                    {
                        points[0].X = (int)(RadiusOfVertex * Math.Cos(Math.PI)) + x1;
                        points[0].Y = (int)(RadiusOfVertex * Math.Sin(Math.PI)) + y1;
                        points[1].X = (int)(RadiusOfVertex * Math.Cos(2 * Math.PI)) + x2;
                        points[1].Y = (int)(RadiusOfVertex * Math.Sin(2 * Math.PI)) + y2;
                    }
                }
                else
                {
                    if (SecondVertexY >= FistVertexY)
                    {
                        points[0].X = (int)(RadiusOfVertex * Math.Cos(3 * Math.PI / 2)) + x1;
                        points[0].Y = -(int)(RadiusOfVertex * Math.Sin(3 * Math.PI / 2)) + y1;
                        points[1].X = (int)(RadiusOfVertex * Math.Cos(Math.PI / 2)) + x2;
                        points[1].Y = -(int)(RadiusOfVertex * Math.Sin(Math.PI / 2)) + y2;
                    }
                    else
                    {
                        points[0].X = (int)(RadiusOfVertex * Math.Cos(Math.PI / 2)) + x1;
                        points[0].Y = -(int)(RadiusOfVertex * Math.Sin(Math.PI / 2)) + y1;
                        points[1].X = (int)(RadiusOfVertex * Math.Cos(3 * Math.PI / 2)) + x2;
                        points[1].Y = -(int)(RadiusOfVertex * Math.Sin(3 * Math.PI / 2)) + y2;
                    }
                }
            }
        }
        public int Number { get { return CurrentNumber; } set { CurrentNumber = value; } }
        public int X1 { get { return points[0].X; } }
        public int Y1 { get { return points[0].Y; } }
        public int X2 { get { return points[1].X; } }
        public int Y2 { get { return points[1].Y; } }
        public bool _IsLoop { get { return IsLoop; } }

        public virtual Graphics Draw(Graphics graf)
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
        public virtual Graphics DrawHighlight(Graphics graf)
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
        public bool Entry(int x, int y)
        {
            if (!IsLoop)
            {
                dy1 = points[1].Y - points[0].Y;
                dx1 = points[1].X - points[0].X;
                dx = x - points[0].X;
                dy = y - points[0].Y;
                S = dx1 * dy - dx * dy1;
                h = S / Math.Sqrt(dx1 * dx1 + dy1 * dy1);
                if (Math.Abs(h) < 4)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                if (Math.Abs(((points[1].X - points[0].X) * (y - points[0].Y) - (x - points[0].X) * (points[1].Y - points[0].Y)) /
                    Math.Sqrt((points[1].X - points[0].X) * (points[1].X - points[0].X) + (points[1].Y - points[0].Y) * (points[1].Y - points[0].Y))) < 4)
                    return true;
                else
                {
                    if ((Math.Abs(((points[2].X - points[1].X) * (y - points[1].Y) - (x - points[1].X) * (points[2].Y - points[1].Y)) /
                    Math.Sqrt((points[2].X - points[1].X) * (points[2].X - points[1].X) + (points[2].Y - points[1].Y) * (points[2].Y - points[1].Y))) < 4))
                        return true;
                    else
                        return false;
                }
            }
        }
    }
}
