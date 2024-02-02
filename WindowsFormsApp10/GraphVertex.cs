using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp10
{
    class GraphVertex
    {
        private int TopLeftX, TopLeftY, Height_Weight, CurrentNumber;
        private Rectangle CurrentRectangle;
        private StringFormat CurrentStringFormat;
        private List<int> OutPutVertex;
        private List<int> InPutVertex;
        private List<int> OutPutInputLines;
        public GraphVertex(int X, int Y, int number)
        {
            this.TopLeftX = X;
            this.TopLeftY = Y;
            this.CurrentNumber = number;
            Height_Weight = 20;
            CurrentRectangle = new Rectangle(TopLeftX - Height_Weight, TopLeftY - Height_Weight, Height_Weight, Height_Weight);
            CurrentStringFormat = new StringFormat();
            CurrentStringFormat.Alignment = StringAlignment.Center;
            CurrentStringFormat.LineAlignment = StringAlignment.Center;
            OutPutVertex = new List<int>();
            InPutVertex = new List<int>();
            OutPutInputLines = new List<int>();
        }
        public int _TopLeftX { get { return TopLeftX; } }
        public int _CurrentNumber { get { return CurrentNumber; } set { CurrentNumber = value; } }
        public int _TopLeftY { get { return TopLeftY; } }
        public int _Height_Weight { get { return Height_Weight; } }
        public int AddOutPutVertex { set { OutPutVertex.Add(value); } }
        public int CountOutPutVertex { get { return OutPutVertex.Count; } }
        public int AddInPutVertex { set { InPutVertex.Add(value); } }
        public int CountInPutVertex { get { return InPutVertex.Count; } }
        public List<int> _InPutVertex { get { return InPutVertex; } }
        public List<int> _OutPutVertex { get { return OutPutVertex; } }
        public int RemoveOutPutVertex { set { if (OutPutVertex.Contains(value)) OutPutVertex.Remove(value); } }
        public int RemoveInPutVertex { set { if (InPutVertex.Contains(value)) InPutVertex.Remove(value); } }
        public int AddOutPutInputLines { set { OutPutInputLines.Add(value); } }
        public int CountOutPutInputLines { get { return OutPutInputLines.Count; } }
        public List<int> _OutPutInputLines { get { return OutPutInputLines; } }
        public int RemoveOutPutInputLines { set { if (OutPutInputLines.Contains(value)) OutPutInputLines.Remove(value); } }


        public Graphics Draw(Graphics graf)
        {
            graf.DrawEllipse(new Pen(Color.Black), CurrentRectangle);
            graf.DrawString(Convert.ToString(CurrentNumber), new Font("Arial", 10), new SolidBrush(Color.Black), CurrentRectangle, CurrentStringFormat);
            return graf;
        }
        public bool Entry(int x, int y)
        {
            double InSide = Math.Sqrt(Math.Pow(x - TopLeftX, 2) + Math.Pow(y - TopLeftY, 2));
            if (InSide <= Height_Weight)
                return true;
            else
                return false;
        }
        public Graphics Highlight(Graphics graf)
        {
            graf.DrawEllipse(new Pen(Color.Red), CurrentRectangle);
            graf.DrawString(Convert.ToString(CurrentNumber), new Font("Arial", 10), new SolidBrush(Color.Red), CurrentRectangle, CurrentStringFormat);
            return graf;
        }
        public int GetLoops()
        {
            int count1 = 0;
            for (int i = 0; i < OutPutVertex.Count; i++)
            {
                if (OutPutVertex[i] == CurrentNumber)
                    count1++;
            }
            return count1;
        }
    }
}
