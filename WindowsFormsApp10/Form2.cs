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
    public partial class Form2 : Form
    {
        int typeOfGraph, countOfVertex, countOfLines, countOfLoops, maxdegree, plusdegree, minusdegree, typeOfConnection;
        int[,] MatrixAdj;
        List<int> numb;
        List<List<int>> list1;
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        List<List<int>> newlist;
        public Form2(int typeOfGraph, int countOfVertex, int countOfLines, int countOfLoops, int maxdegree, int plusdegree, int minusdegree, int typeOfConnection, List<List<int>> newlist, List<int> numb, int[,] MatrixAdj, List<List<int>> list1)
        {
            InitializeComponent();
            this.typeOfGraph = typeOfGraph;
            this.countOfVertex = countOfVertex;
            this.countOfLines = countOfLines;
            this.countOfLoops = countOfLoops;
            this.maxdegree = maxdegree;
            this.plusdegree = plusdegree;
            this.minusdegree = minusdegree;
            this.typeOfConnection = typeOfConnection;
            this.newlist = newlist;
            this.numb = numb;
            this.MatrixAdj = MatrixAdj;
            this.list1 = list1;
            label14.Hide();
            label15.Hide();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if(typeOfGraph == 1)
            {
                label13.Text = "Неориентированный";
                label3.Text = "2. Количество рёбер: ";
                label10.Text = Convert.ToString(maxdegree);
                if (typeOfConnection == 2)
                    label11.Text = "Связный";
                if (typeOfConnection == 1)
                    label11.Text = "Несвязный";
                label14.Show();
                label15.Show();
                label17.Text = "Список рёбер";
                label15.Text = $"Количество: {Convert.ToString(newlist[0][0])}. Список вершин: {obr()} ";
            }
            if(typeOfGraph == 2)
            {
                label13.Text = "Ориентированный";
                label3.Text = "2. Количество дуг: ";
                label17.Text = "Список дуг";
                label10.Text = $"По заходам: {plusdegree}, по исходам {minusdegree}";
                if (typeOfConnection == 1)
                    label11.Text = "Несвязный";
                if (typeOfConnection == 2)
                    label11.Text = "Слабо связный";
                if (typeOfConnection == 3)
                    label11.Text = "Односторонне связный";
                if (typeOfConnection == 4)
                    label11.Text = "Сильно связный";
            }
            label7.Text = Convert.ToString(countOfVertex);
            label8.Text = Convert.ToString(countOfLines);
            label9.Text = Convert.ToString(countOfLoops);
            matr();
            listofedges();

        }
        private string obr()
        {
            string s = "";
            for(int i = 1; i < newlist.Count; i++)
            {
                for(int j = 0; j < newlist[i].Count; j++)
                {
                    s += Convert.ToString(newlist[i][j]);
                    if(j != newlist[i].Count - 1)
                        s += ",";
                }
                s += "|||";
            }
            return s;
        }
        private void matr()
        {
            for (int i = 0; i < numb.Count; i++)
            {
                dataGridView1.Columns.Add(Convert.ToString(numb[i]), Convert.ToString(numb[i]));
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].HeaderCell.Value = Convert.ToString(numb[i]);
            }
            dataGridView1.AllowUserToAddRows = false;
            for (int i = 0; i < numb.Count; i++)
            {
                for (int j = 0; j < numb.Count; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = MatrixAdj[i,j];
                }
            }
        }
        private void listofedges()
        {
            List<int> dd = new List<int>() { };
            string s = "";
            for(int i = 0; i < list1.Count; i++)
            {
                for(int j = 0; j < list1[i].Count; j++)
                {
                    if(typeOfGraph == 1)
                    {
                        if(!dd.Contains(list1[i][j]))
                        {
                            s += Convert.ToString($"{numb[i]} - {list1[i][j]}");
                            if (j != list1[i].Count - 1)
                                s += ";";
                            else
                                s += "\r\n";
                        }
                    }
                    if(typeOfGraph == 2)
                    {
                        s += Convert.ToString($"{numb[i]} - {list1[i][j]}");
                        if (j != list1[i].Count - 1)
                            s += ";";
                        else
                            s += "\r\n";
                    }
                }
                if(s != "")
                    listBox1.Items.Add(s);
                s = "";
                dd.Add(numb[i]);
            }
        }
    }
}
