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
    public partial class Form3 : Form
    {
        List<List<int>> gamg;
        int typeofgraph;
        List<int> dirak, ore, gooyauri;
        public Form3(List<List<int>> gamg, int typeofgraf,bool svyaz, List<int> dirak, List<int> ore, List<int> gooyauri, bool loop, int countOfVert)
        {
            InitializeComponent();
            this.dirak = dirak;
            this.ore = ore;
            this.gooyauri = gooyauri;
            this.gamg = gamg;
            this.typeofgraph = typeofgraf;
            if(loop || countOfVert < 3)
            {
                gamg.Clear();
            }    
            if (this.typeofgraph == 2)
            {
                checkBox2.Enabled = false;
                checkBox7.Enabled = false;
                checkBox3.Enabled = false;
                checkBox5.Enabled = false;
                checkBox10.Enabled = false;
                label6.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
            }
            if(this.typeofgraph == 1)
            {
                checkBox8.Enabled = false;
                checkBox9.Enabled = false;
                checkBox4.Enabled = false;
                checkBox6.Enabled = false;
                label7.Enabled = false;
                button3.Enabled = false;
            }
            if(gamg.Count > 0)
            {
                checkBox1.Checked = true;
                label3.Text = Convert.ToString(gamg.Count);
            }
            
            for(int i = 0; i < gamg.Count; i++)
            {
                for (int j = 0; j < gamg[i].Count; j++)
                {
                    richTextBox1.Text += gamg[i][j];
                }
                richTextBox1.Text += "\r\n";
            }
            if(typeofgraf == 1)
            {
                if (svyaz)
                    checkBox5.Checked = true;
                if (dirak[0] == 2)
                    checkBox2.Checked = true;
                if (ore[0] == 2)
                    checkBox3.Checked = true;
                if (loop)
                    checkBox7.Checked = true;
                if (countOfVert >= 3)
                    checkBox10.Checked = true;

            }
            if(typeofgraf == 2)
            {
                if (svyaz)
                    checkBox6.Checked = true;
                if (gooyauri[0] == 2)
                    checkBox4.Checked = true;
                if (loop)
                    checkBox8.Checked = true;
                if (countOfVert >= 3)
                    checkBox9.Checked = true;
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (gooyauri[0] == 2)
            {
                MessageBox.Show(
                        "Орсвязный граф обладает гамильтоновым циклом, если deg+ >= n/2, deg− >= n/2.\r\n",
                        "Теорема Гуйя-Ури( выполняется)",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show(
                        $"Вершина {gooyauri[1]} имеет степень+ {gooyauri[2]}, степень- {gooyauri[3]} что < n/2 .\r\n",
                        "Теорема Гуйя-Ури(не выполняется)",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(dirak[0] == 2)
            {
                MessageBox.Show(
                        "Граф гамильтонов, если количество его вершин >=3 и степень любой его вершины удовлетворяет неравенству deg v > n/2.\r\n",
                        "Теорема Дирака( выполняется)",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
            }
            if (dirak[0] == 1)
            {
                MessageBox.Show(
                        $"Вершина {dirak[1]} имеет степень {dirak[2]}, что < n/2 .\r\n",
                        "Теорема Дирака(не выполняется)",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
            }
            if (dirak[0] == 3)
            {
                MessageBox.Show(
                        $"Количество вершин < 3.\r\n",
                        "Теорема Дирака(не выполняется)",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ore[0] == 2)
            {
                MessageBox.Show(
                        "Граф гамильтонов, если количество его вершин >=3 и степени любых двух его несмежных вершин v и u удовлетворяет неравенству deg v + deg u > n.\r\n",
                        "Теорема Оре( выполняется)",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
            }
            if(ore[0] == 1)
            {
                MessageBox.Show(
                        $"Вершина {ore[1]} имеет степень {ore[2]}, вершина {ore[3]} имеет степень {ore[4]} что < n .\r\n",
                        "Теорема Оре(не выполняется)",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
            }
            if (ore[0] == 3)
            {
                MessageBox.Show(
                        $"Количество вершин < 3.\r\n",
                        "Теорема Оре(не выполняется)",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
            }
        }
    }
}
