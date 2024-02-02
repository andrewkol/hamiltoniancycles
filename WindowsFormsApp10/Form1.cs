using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp10
{
    public partial class Form1 : Form
    {
        Graphics graf;
        Graph RealGraph;
        int StartX, StartY, FirstIndex, SecondIndex, LineIndex;
        bool IsLoop;
        GraphVertex Vertex1, Vertex2;

        private void button3_Click(object sender, EventArgs e)
        {
            PanelClear();
            if (RealGraph != null)
                RealGraph.Draw(graf);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
        "Колач Андрей 2221",
        "Автор",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information,
        MessageBoxDefaultButton.Button1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
        "Клик левой кнопкой мыши по рабочей области.",
        "Добавление вершины",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information,
        MessageBoxDefaultButton.Button1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
        "Клик левой кнопкой мыши по необходимой вершине.\r\n" +
        "Далее будет выделена вершина, необходимо подтвердить удаление.",
        "Удаление вершины",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information,
        MessageBoxDefaultButton.Button1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
        "Левый клик по первой вершине. Она выделится красным цветом.\r\n" +
        "Далее левый клик по второй вершине.",
        "Добавление ребра(дуги)",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information,
        MessageBoxDefaultButton.Button1);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            PanelClear();
            RealGraph = new Graph();
            radioButton5.Text = "Добавить ребро";
            radioButton6.Text = "Удалить ребро";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            PanelClear();
            RealGraph = new Graph();
            radioButton5.Text = "Добавить дугу";
            radioButton6.Text = "Удалить дугу";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
        "Правый клик по нужному ребру(дуге).\r\n" +
        "Далее будет выделено ребро(дуга), необходимо подтвердить удаление.",
        "Удаление ребра(дуги)",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information,
        MessageBoxDefaultButton.Button1);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
        "Выбираете тип графа: граф, орграф.\r\n" +
        "Добавляете вершины.\r\n" +
        "Добавляете дуги, рёбра.\r\n" +
        "Получаете результат кнопкой 'Результат'.\r\n" +
        "Кнопка 'Очистить' удаляет созданный граф и очищает рабочую область.\r\n" +
        "Кнопка 'Перерисовка' очищает рабочую область и заново рисует граф.\r\n",
        "Помощь",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information,
        MessageBoxDefaultButton.Button1);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (RealGraph.CurrentNumber > 0)
            {
                bool svyaz = false;
                if (radioButton1.Checked)
                {
                    RealGraph.MainGamilt();
                    if (RealGraph.TypeOfConnectForEdge() == 2)
                        svyaz = true;
                    Form3 form3 = new Form3(RealGraph._gamgraph, 1,svyaz, RealGraph.Dirak(), RealGraph.Ore(), RealGraph.Guya_Uri(), RealGraph.LoopHere(), RealGraph.CountOfVertex());
                    form3.Show();
                }
                if (radioButton2.Checked)
                {
                    if (RealGraph.TypeOfConnectforArc() == 4)
                        svyaz = true;
                    RealGraph.MainGamilt();
                    Form3 form3 = new Form3(RealGraph._gamgraph, 2, svyaz, RealGraph.Dirak(), RealGraph.Ore(), RealGraph.Guya_Uri(), RealGraph.LoopHere(), RealGraph.CountOfVertex());
                    form3.Show();
                }
            }    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PanelClear();
            RealGraph = new Graph();
        }

        public Form1()
        {
            InitializeComponent();
            graf = panel3.CreateGraphics();
            RealGraph = new Graph();
            radioButton2.Checked = false;
            radioButton1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(RealGraph.CurrentNumber > 0)
            {
                if(radioButton1.Checked)
                {
                    Form2 form2 = new Form2(1, RealGraph.CountOfVertex(), RealGraph.CountOfLines(), RealGraph.CountOfLoops(), RealGraph.MaxDegreeForEdge(), -1, -1, RealGraph.TypeOfConnectForEdge(), RealGraph.CompConnect(true), RealGraph.AllNumbers(), RealGraph.MatrixAdjacency(), RealGraph.ListOfEdges());
                    form2.Show();
                }
                if(radioButton2.Checked)
                {
                    Form2 form2 = new Form2(2, RealGraph.CountOfVertex(), RealGraph.CountOfLines(), RealGraph.CountOfLoops(), -1, RealGraph.MaxInputDegreeForArc(), RealGraph.MaxOutputDegreeForArc(), RealGraph.TypeOfConnectforArc(), RealGraph.CompConnect(false), RealGraph.AllNumbers(), RealGraph.MatrixAdjacency(), RealGraph.ListOfEdges());
                    form2.Show();
                }
                RealGraph.SecondMeth();
                RealGraph.MainGamilt();
            }
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            IsLoop = false;
            if(radioButton4.Checked && e.Button == MouseButtons.Left)
            {
                PanelClear();
                RealGraph.AddVertex(e.X, e.Y);
                RealGraph.Draw(graf);
            }
            if(radioButton3.Checked && e.Button == MouseButtons.Right)
            {
                if(RealGraph.InVertexCheck(e.X, e.Y) > -1)
                {
                    RealGraph.DrawWithHighligt(graf, RealGraph.InVertexCheck(e.X, e.Y));
                    DialogResult = MessageBox.Show(
                            "Выделена нужная вершина?",
                            "Удаление вершины",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
                    if (DialogResult == DialogResult.Yes)
                    {
                        RealGraph.RemoveVertex(RealGraph.InVertexCheck(e.X, e.Y));
                        PanelClear();
                        RealGraph.Draw(graf);
                    }
                    else
                    {
                        PanelClear();
                        RealGraph.Draw(graf);
                    }
                }
            }
            if(radioButton5.Checked && e.Button == MouseButtons.Left)
            {
                if(radioButton1.Checked)
                {
                    if(StartX == 0 && StartY == 0)
                    {
                        StartX = e.X;
                        StartY = e.Y;
                        FirstIndex = RealGraph.InVertexCheck(StartX, StartY);
                        PanelClear();
                        RealGraph.DrawWithHighligt(graf, FirstIndex);
                        if(FirstIndex == -1)
                        {
                            StartX = 0;
                            StartY = 0;
                        }
                    }
                    else
                    {
                        SecondIndex = RealGraph.InVertexCheck(e.X, e.Y);
                        Vertex1 = RealGraph.GetVertex(FirstIndex);
                        Vertex2 = RealGraph.GetVertex(SecondIndex);
                        if (FirstIndex == SecondIndex)
                            IsLoop = true;
                        if(RealGraph.GetVertex(FirstIndex) != null && RealGraph.GetVertex(SecondIndex) != null)
                        {
                            RealGraph.AddEdge(Vertex1._TopLeftX - Vertex1._Height_Weight / 2, Vertex1._TopLeftY - Vertex1._Height_Weight / 2, Vertex2._TopLeftX - Vertex2._Height_Weight / 2, Vertex2._TopLeftY - Vertex2._Height_Weight / 2, IsLoop);
                            RealGraph.AddOutputVertexForEdge(FirstIndex, SecondIndex);
                            PanelClear();
                            RealGraph.Draw(graf);
                            StartX = 0;
                            StartY = 0;
                            Vertex1 = null;
                            Vertex2 = null;
                        }
                    }
                }
                if(radioButton2.Checked)
                {
                    if (StartX == 0 && StartY == 0)
                    {
                        StartX = e.X;
                        StartY = e.Y;
                        FirstIndex = RealGraph.InVertexCheck(StartX, StartY);
                        PanelClear();
                        RealGraph.DrawWithHighligt(graf, FirstIndex);
                        if (FirstIndex == -1)
                        {
                            StartX = 0;
                            StartY = 0;
                        }
                    }
                    else
                    {
                        SecondIndex = RealGraph.InVertexCheck(e.X, e.Y);
                        Vertex1 = RealGraph.GetVertex(FirstIndex);
                        Vertex2 = RealGraph.GetVertex(SecondIndex);
                        if (FirstIndex == SecondIndex)
                            IsLoop = true;
                        if (RealGraph.GetVertex(FirstIndex) != null && RealGraph.GetVertex(SecondIndex) != null)
                        {
                            RealGraph.AddArc(Vertex1._TopLeftX - Vertex1._Height_Weight / 2, Vertex1._TopLeftY - Vertex1._Height_Weight / 2, Vertex2._TopLeftX - Vertex2._Height_Weight / 2, Vertex2._TopLeftY - Vertex2._Height_Weight / 2, IsLoop);
                            RealGraph.AddOutputVertexForArc(FirstIndex, SecondIndex);
                            PanelClear();
                            RealGraph.Draw(graf);
                            StartX = 0;
                            StartY = 0;
                            Vertex1 = null;
                            Vertex2 = null;
                        }
                    }
                }
            }
            if(radioButton6.Checked && e.Button == MouseButtons.Right)
            {
                LineIndex = RealGraph.InLineCheck(e.X, e.Y);
                if(LineIndex > -1)
                {
                    PanelClear();
                    RealGraph.DrawWithHighligtLine(graf, LineIndex);
                    DialogResult = MessageBox.Show(
                            "Выделено нужное ребро(дуга)?",
                            "Удаление ребра(дуги)",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
                    if (DialogResult == DialogResult.Yes)
                    {
                        RealGraph.RemoveLine(LineIndex);
                        PanelClear();
                        RealGraph.Draw(graf);
                    }
                    else
                    {
                        PanelClear();
                        RealGraph.Draw(graf);
                    }
                }
            }
        }
        private void PanelClear()
        {
            graf.Clear(BackColor);
        }
    }
}
