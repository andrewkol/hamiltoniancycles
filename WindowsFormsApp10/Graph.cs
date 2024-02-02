using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp10
{
    class Graph
    {
       private List<GraphVertex> AllVertex;
       private List<GraphLine> AllLines;
       private List<Int32>[] listOfArrayforDFS;
       private bool[] visitedforReachabilityMatrix;
       private int[,] reachabilityMatrix;
       private List<int> l1;
       private List<List<int>> lisst2;
       private int[] X;
       private bool[] dop;
       private List<List<int>> gamgraph;
        public Graph()
        {
            AllVertex = new List<GraphVertex>();
            AllLines = new List<GraphLine>();
            reachabilityMatrix = new int[AllVertex.Count, AllVertex.Count];
            listOfArrayforDFS = new List<Int32>[AllVertex.Count];
        }
        public int CurrentNumber { get { return AllVertex.Count; } }
        public List<List<int>> _gamgraph { get { return gamgraph; } }
        public void AddVertex(int x, int y)
        {
            if (AllVertex.Count < 1)
                AllVertex.Add(new GraphVertex(x, y, 0));
            else
                AllVertex.Add(new GraphVertex(x, y, AllVertex[AllVertex.Count - 1]._CurrentNumber + 1));
        }
        public void AddEdge(int StartX, int StartY, int EndX, int EndY, bool loop)
        {
            if (AllLines.Count < 1)
                AllLines.Add(new GraphEdge(StartX, StartY, EndX, EndY, loop, 10, 0));
            else
                AllLines.Add(new GraphEdge(StartX, StartY, EndX, EndY, loop, 10, AllLines[AllLines.Count-1].Number + 1));
        }
        public void AddArc(int StartX, int StartY, int EndX, int EndY, bool loop)
        {
            if (AllLines.Count < 1)
                AllLines.Add(new GraphArc(StartX, StartY, EndX, EndY, loop, 10, 0));
            else
                AllLines.Add(new GraphArc(StartX, StartY, EndX, EndY, loop, 10, AllLines[AllLines.Count - 1].Number + 1));
        }
        public void RemoveVertex(int n)
        {
            int DelNumber = AllVertex[n]._CurrentNumber;
            List<int> DelLines = AllVertex[n]._OutPutInputLines;
            for(int i = 0; i < AllVertex.Count; i++)
            {
                if(i != n)
                {
                    if (AllVertex[i]._InPutVertex.Contains(DelNumber))
                        AllVertex[i].RemoveInPutVertex = DelNumber;
                    if (AllVertex[i]._OutPutVertex.Contains(DelNumber))
                        AllVertex[i].RemoveOutPutVertex = DelNumber;
                    List<int> ListToDelete = new List<int>();
                    for (int j = 0; j < AllVertex[i].CountOutPutInputLines; j++)
                    {
                        List<int> ListOfLines = AllVertex[i]._OutPutInputLines;
                        for(int h = 0; h < DelLines.Count; h++)
                        {
                            if (ListOfLines[j] == DelLines[h])
                                ListToDelete.Add(ListOfLines[j]);
                        }
                        if(ListToDelete.Count > 1)
                        {
                            for (int v = 0; v < ListToDelete.Count; v++)
                            {
                                AllVertex[i].RemoveOutPutInputLines = ListToDelete[v];
                            }
                            ListToDelete = null;
                        }
                    }
                }
            }
            for(int i = 0; i < AllLines.Count; i++)
            {
                for (int h = 0; h < DelLines.Count; h++)
                {
                    if (AllLines[i].Number == DelLines[h])
                        AllLines.RemoveAt(i);
                }
            }
            AllVertex.RemoveAt(n);
        }
        public void RemoveLine(int n)
        {
            int Delnum = AllLines[n].Number;
            List<int> DelLines;
            List<int> VertexToCheck = new List<int>();
            for (int i = 0; i < AllVertex.Count; i++)
            {
                DelLines = AllVertex[i]._OutPutInputLines;
                if (DelLines.Contains(Delnum))
                    VertexToCheck.Add(i);
            }
            if (VertexToCheck.Count > 1)
            {
                int Delnum1 = VertexToCheck[0];
                int Delnum2 = VertexToCheck[1];   
                if(AllLines[n].GetType() == typeof(GraphEdge))
                {
                    AllVertex[Delnum1].RemoveInPutVertex = AllVertex[Delnum2]._CurrentNumber;
                    AllVertex[Delnum1].RemoveOutPutVertex = AllVertex[Delnum2]._CurrentNumber;
                    AllVertex[Delnum2].RemoveInPutVertex = AllVertex[Delnum1]._CurrentNumber;
                    AllVertex[Delnum2].RemoveOutPutVertex = AllVertex[Delnum1]._CurrentNumber;
                    AllVertex[Delnum1].RemoveOutPutInputLines = Delnum;
                    AllVertex[Delnum2].RemoveOutPutInputLines = Delnum;
                }
                else
                {
                    List<int> nh = AllVertex[Delnum1]._OutPutVertex;
                    if (nh.Contains(AllVertex[Delnum2]._CurrentNumber))
                    {
                        AllVertex[Delnum1].RemoveOutPutVertex = AllVertex[Delnum2]._CurrentNumber;
                        AllVertex[Delnum2].RemoveInPutVertex = AllVertex[Delnum1]._CurrentNumber;
                        AllVertex[Delnum1].RemoveOutPutInputLines = Delnum;
                        AllVertex[Delnum2].RemoveOutPutInputLines = Delnum;
                    }
                    else
                    {
                        AllVertex[Delnum1].RemoveInPutVertex = AllVertex[Delnum2]._CurrentNumber;
                        AllVertex[Delnum2].RemoveOutPutVertex = AllVertex[Delnum1]._CurrentNumber;
                        AllVertex[Delnum1].RemoveOutPutInputLines = Delnum;
                        AllVertex[Delnum2].RemoveOutPutInputLines = Delnum;
                    }
                }    
            }
            else
            {
                int Delnum1 = VertexToCheck[0];
                AllVertex[Delnum1].RemoveInPutVertex = AllVertex[Delnum1]._CurrentNumber;
                AllVertex[Delnum1].RemoveOutPutVertex = AllVertex[Delnum1]._CurrentNumber;
                AllVertex[Delnum1].RemoveOutPutInputLines = Delnum;
            }
            AllLines.RemoveAt(n);
        }
        public Graphics Draw(Graphics graf)
        {
            for(int i = 0; i < AllVertex.Count; i++)
            {
                AllVertex[i].Draw(graf);
            }
            for (int i = 0; i < AllLines.Count; i++)
            {
                AllLines[i].Draw(graf);
            }
            return graf;
        }
        public Graphics DrawWithHighligt(Graphics graf, int n)
        {
            for (int i = 0; i < AllVertex.Count; i++)
            {
                if (i == n)
                    AllVertex[i].Highlight(graf);
                else
                    AllVertex[i].Draw(graf);
            }
            for (int i = 0; i < AllLines.Count; i++)
            {
                AllLines[i].Draw(graf);
            }
            return graf;
        }
        public Graphics DrawWithHighligtLine(Graphics graf, int n)
        {
            for (int i = 0; i < AllVertex.Count; i++)
            {
                AllVertex[i].Draw(graf);
            }
            for (int i = 0; i < AllLines.Count; i++)
            {
                if(i == n)
                {
                    AllLines[i].DrawHighlight(graf);
                }
                else
                    AllLines[i].Draw(graf);
            }
            return graf;
        }
        public int InVertexCheck(int x, int y)
        {
            for(int i = 0; i < AllVertex.Count; i++)
            {
                if (AllVertex[i].Entry(x, y))
                    return i;
            }
            return -1;
        }
        public int InLineCheck(int x, int y)
        {
            for (int i = 0; i < AllLines.Count; i++)
            {
                if (AllLines[i].Entry(x, y))
                    return i;
            }
            return -1;
        }
        public GraphVertex GetVertex(int n)
        {
            try
            {
                return AllVertex[n];
            }
            catch
            {
                return null;
            }
        }
        public void AddOutputVertexForEdge(int n, int m)
        {
            if (n != m)
            {
                AllVertex[n].AddOutPutVertex = AllVertex[m]._CurrentNumber;
                AllVertex[m].AddOutPutVertex = AllVertex[n]._CurrentNumber;
                AllVertex[n].AddOutPutInputLines = AllLines[AllLines.Count - 1].Number;
                AllVertex[m].AddOutPutInputLines = AllLines[AllLines.Count - 1].Number;
            }
            else
            {
                AllVertex[n].AddOutPutVertex = AllVertex[m]._CurrentNumber;
                AllVertex[n].AddOutPutInputLines = AllLines[AllLines.Count - 1].Number;
            }
        }
        public void AddOutputVertexForArc(int n, int m)
        {
            if (n != m)
            {
                AllVertex[n].AddOutPutVertex = AllVertex[m]._CurrentNumber;
                AllVertex[m].AddInPutVertex = AllVertex[n]._CurrentNumber;
                AllVertex[n].AddOutPutInputLines = AllLines[AllLines.Count - 1].Number;
                AllVertex[m].AddOutPutInputLines = AllLines[AllLines.Count - 1].Number;
            }
            else
            {
                AllVertex[n].AddInPutVertex = AllVertex[m]._CurrentNumber;
                AllVertex[n].AddOutPutVertex = AllVertex[m]._CurrentNumber;
                AllVertex[n].AddOutPutInputLines = AllLines[AllLines.Count - 1].Number;
            }
        }
        public int CountOfVertex()
        {
            return AllVertex.Count;
        }
        public int CountOfLines()
        {
            return AllLines.Count;
        }
        public int CountOfLoops()
        {
            int count = 0;
            for(int i = 0; i < AllVertex.Count; i++)
            {
                count += AllVertex[i].GetLoops();
            }
            return count;
        }
        public int MaxDegreeForEdge()
        {
            int maxdegree = -1;
            for (int i = 0; i < AllVertex.Count; i++)
            {
                if (AllVertex[i].CountOutPutVertex + AllVertex[i].GetLoops() > maxdegree)
                    maxdegree = AllVertex[i].CountOutPutVertex + AllVertex[i].GetLoops();
            }
            return maxdegree;
        }
        public int MaxOutputDegreeForArc()
        {
            int maxdegree = -1;
            for (int i = 0; i < AllVertex.Count; i++)
            {
                if (AllVertex[i].CountOutPutVertex > maxdegree)
                    maxdegree = AllVertex[i].CountOutPutVertex;
            }
            return maxdegree;
        }
        public int MaxInputDegreeForArc()
        {
            int maxdegree = -1;
            for (int i = 0; i < AllVertex.Count; i++)
            {
                if (AllVertex[i].CountInPutVertex > maxdegree)
                    maxdegree = AllVertex[i].CountInPutVertex;
            }
            return maxdegree;
        }
        public int TypeOfConnectForEdge()
        {
            OneMeth();
            if (DFS(AllVertex[0]._CurrentNumber) == AllVertex.Count)
                return 2;
            else
                return 1;
        }
        private int DFS(int s)
        {
            visitedforReachabilityMatrix = new bool[AllVertex.Count];
            int trueindex;
            int countofvisited = 0;
            Stack<int> stack = new Stack<int>();
            l1 = new List<int>();
            trueindex = FindIndexofNum(s);
            visitedforReachabilityMatrix[trueindex] = true;
            stack.Push(s);
            while (stack.Count != 0)
            {
                s = stack.Pop();
                l1.Add(s);
                foreach (int i in listOfArrayforDFS[FindIndexofNum(s)])
                {
                    if (!visitedforReachabilityMatrix[FindIndexofNum(i)])
                    {
                        visitedforReachabilityMatrix[FindIndexofNum(i)] = true;
                        stack.Push(i);
                    }
                }
            }
            countofvisited = visitedforReachabilityMatrix.Count(x => x.Equals(true));
            return countofvisited;
        }
        private int FindIndexofNum(int num)
        {
            for (int i = 0; i < AllVertex.Count; i++)
            {
                if (AllVertex[i]._CurrentNumber == num)
                {
                    return i;
                }
            }
            return -1;
        }
        public int TypeOfConnectforArc()
        {
            int visi, max, maxc;
            OneMeth();
            MatrixReachib();
            int[] visiter = new int[AllVertex.Count];
            visi = 0;
            for(int i = 0; i < visiter.Length; i++)
            {
                for(int j = 0; j < AllVertex.Count; j++)
                {
                    if (reachabilityMatrix[i, j] == 1)
                        visi++;
                }
                visiter[i] = visi;
                visi = 0;
            }
            max = visiter.Max();
            maxc = visiter.Count(x => x.Equals(max));
            if (Newmeth())
            {
                if (max == AllVertex.Count && maxc == AllVertex.Count)
                    return 4;
                else
                {
                    if (CheckConnect())
                        return 3;
                    else
                        return 2;
                }
            }
            else
                return 1;
        }
        private bool Newmeth()
        {
            listOfArrayforDFS = new List<Int32>[AllVertex.Count];
            for (int i = 0; i < AllVertex.Count; i++)
            {
                listOfArrayforDFS[i] = new List<int>() { };
                foreach (var item in AllVertex[i]._OutPutVertex)
                {
                    listOfArrayforDFS[i].Add(item);
                }
            }
            for (int i = 0; i < listOfArrayforDFS.Length; i++)
            {
                List<int> r1 = AllVertex[i]._InPutVertex;
                for (int j = 0; j < r1.Count; j++)
                {
                    if (!listOfArrayforDFS[i].Contains(r1[j]))
                    {
                        listOfArrayforDFS[i].Add(r1[j]);
                    }
                }
            }
            
            if (DFS(AllVertex[0]._CurrentNumber) == AllVertex.Count)
                return true;
            else
                return false;
        }
        private void OneMeth()
        {
            visitedforReachabilityMatrix = new bool[AllVertex.Count];
            listOfArrayforDFS = new List<Int32>[AllVertex.Count];
            for (int i = 0; i < AllVertex.Count; i++)
            {
                listOfArrayforDFS[i] = AllVertex[i]._OutPutVertex;
            }
        }
        public void SecondMeth()
        {
            reachabilityMatrix = null;
            listOfArrayforDFS = null;
            reachabilityMatrix = null;
            visitedforReachabilityMatrix = null;
            listOfArrayforDFS = new List<Int32>[AllVertex.Count];
            for (int i = 0; i < AllVertex.Count; i++)
            {
                listOfArrayforDFS[i] = AllVertex[i]._OutPutVertex;
            }
        }
        private void MatrixReachib()
        {
            reachabilityMatrix = new int[AllVertex.Count, AllVertex.Count];
            for (int i = 0; i < AllVertex.Count; i++)
            {
                DFS(AllVertex[i]._CurrentNumber);
                for (int j = 0; j < AllVertex.Count; j++)
                {
                    if (visitedforReachabilityMatrix[j])
                        reachabilityMatrix[i, j] = 1;
                    else
                        reachabilityMatrix[i, j] = 0;
                }
            }
        }
        private bool CheckConnect()
        {
            int iter, reachibility;
            for (int i = 1; i < AllVertex.Count; i++)
            {
                iter = 0;
                reachibility = 0;
                int v = i;
                int standart = 0;
                int h = 0;
                while (v > -1)
                {
                    if (h != v)
                        iter++;
                    if (reachabilityMatrix[h, v] == standart)
                    {
                        if (h != v)
                            reachibility++;
                    }
                    v--;
                    h++;
                }
                if (reachibility == iter)
                    return false;
            }
            for (int i = AllVertex.Count - 2; i > 0; i--)
            {
                iter = 0;
                reachibility = 0;
                int v = i;
                int standart = 0;
                int h = AllVertex.Count - 1;
                while (v <= AllVertex.Count - 1)
                {
                    if(h != v)
                        iter++;
                    if (reachabilityMatrix[h, v] == standart)
                    {
                        if (h != v)
                            reachibility++;
                    }
                    v++;
                    h--;
                }
                if (reachibility == iter)
                    return false;
            }
            return true;
        }
        public List<List<int>> CompConnect(bool f)
        {
            List<int> newlist = new List<int>();
            List<List<int>> newlist1 = new List<List<int>>();
            if (f)
            {
                
                for (int i = 0; i < AllVertex.Count; i++)
                {
                    newlist.Add(AllVertex[i]._CurrentNumber);
                }
                DFS(newlist[0]);
                newlist1.Add(new List<int>() { 1 });
                newlist1.Add(l1);
                int count2 = 0;
                for (int i = 1; i < newlist.Count; i++)
                {
                    if (!l1.Contains(newlist[i]))
                    {
                        DFS(AllVertex[i]._CurrentNumber);
                        for (int j = 1; j < newlist1.Count; j++)
                        {
                            count2 = 0;
                            for (int v = 0; v < l1.Count; v++)
                            {
                                if (newlist1[j].Contains(l1[v]))
                                {
                                    count2++;
                                }
                            }
                            if (count2 == l1.Count)
                                break;
                        }
                        if (count2 != l1.Count)
                        {
                            newlist1.Add(l1);
                            newlist1[0][0] += 1;
                        }
                    }
                }
                return newlist1;
            }
            else
            {
                return newlist1;
            }
        }
        public int[,] MatrixAdjacency()
        {
            int[,] MatrixAdj = new int[AllVertex.Count, AllVertex.Count];
            List<int> h1 = new List<int>() { };
            for (int i = 0; i < AllVertex.Count; i++)
            {
                foreach(var item in AllVertex[i]._OutPutVertex)
                {
                    h1.Add(item);
                }
                for(int j = 0; j < AllVertex.Count; j++)
                {
                    while(h1.Contains(AllVertex[j]._CurrentNumber))
                    {
                        MatrixAdj[i, j] += 1;
                        h1.Remove(AllVertex[j]._CurrentNumber);
                    }
                }
            }
            return MatrixAdj;
        }
        public List<int> AllNumbers()
        {
            List<int> numb = new List<int>() { };
            for(int i = 0; i < AllVertex.Count; i++)
            {
                numb.Add(AllVertex[i]._CurrentNumber);
            }
            return numb;
        }
        public List<List<int>> ListOfEdges()
        {
            List<List<int>> list1 = new List<List<int>>() { };
            for(int i = 0; i < AllVertex.Count; i++)
            {
                list1.Add(AllVertex[i]._OutPutVertex);
            }
            return list1;
        }
        private void Gamilt(int k)
        {
            foreach(int item in lisst2[X[k - 1]])
            {
                int n = AllVertex.Count;
                int v0 = AllVertex[0]._CurrentNumber;
                if (k == n && item == v0)
                {
                    gamgraph.Add(new List<int>() { });
                    for(int i = 0; i < X.Length; i++)
                    {
                        gamgraph[gamgraph.Count - 1].Add(X[i]);
                    }
                    gamgraph[gamgraph.Count - 1].Add(v0);
                }
                else
                {
                    if(dop[FindIndexofNum(item)])
                    {
                        X[k] = FindIndexofNum(item);
                        dop[FindIndexofNum(item)] = false;
                        Gamilt(k + 1);
                        dop[FindIndexofNum(item)] = true;
                    }
                }
            }
        }
        public void MainGamilt()
        {
            gamgraph = new List<List<int>>() { };
            X = new int[AllVertex.Count];
            lisst2 = new List<List<int>>() { };
            dop = new bool[AllVertex.Count];
            for (int i = 0; i < AllVertex.Count; i++)
            {
                lisst2.Add(AllVertex[i]._OutPutVertex);
                X[i] = AllVertex[i]._CurrentNumber;
            }
            for (int i = 0; i < AllVertex.Count; i++)
            {
                dop[i] = true;
            }
            X[0] = AllVertex[0]._CurrentNumber;
            dop[0] = false;
            Gamilt(1);
        }
        public bool LoopHere()
        {
            if (CountOfLoops() > 0)
                return true;
            return false;
        }
        public List<int> Dirak()
        {
            List<int> reallist = new List<int>() { };
            if (AllVertex.Count >= 3)
            {
                for (int i = 0; i < AllVertex.Count; i++)
                {
                    if (AllVertex[i].CountOutPutVertex + AllVertex[i].GetLoops() * 2 >= AllVertex.Count / 2)
                        continue;
                    else
                    {
                        reallist.Add(1);
                        reallist.Add(AllVertex[i]._CurrentNumber);
                        reallist.Add(AllVertex[i].CountOutPutVertex + AllVertex[i].GetLoops() * 2);
                        return reallist;
                    }
                }
                reallist.Add(2);
                return reallist;
            }
            else
            {
                reallist.Add(3);
                return reallist;
            }
        }
        public List<int> Ore()
        {
            List<int> reallist = new List<int>() { };
           if(AllVertex.Count >= 3)
            {
                for (int i = 0; i < AllVertex.Count - 1; i++)
                {
                    for (int j = 1; j < AllVertex.Count; j++)
                    {
                        List<int> s = AllVertex[i]._OutPutVertex;
                        if (!s.Contains(AllVertex[j]._CurrentNumber))
                        {
                            if (AllVertex[i].CountOutPutVertex + AllVertex[i].GetLoops() * 2 +
                                AllVertex[j].CountOutPutVertex + AllVertex[j].GetLoops() * 2  >= AllVertex.Count)
                                continue;
                            else
                            {
                                reallist.Add(1);
                                reallist.Add(AllVertex[i]._CurrentNumber);
                                reallist.Add(AllVertex[i].CountOutPutVertex + AllVertex[i].GetLoops() * 2);
                                reallist.Add(AllVertex[j]._CurrentNumber);
                                reallist.Add(AllVertex[j].CountOutPutVertex + AllVertex[j].GetLoops() * 2);
                            }

                        }
                    }
                }
                reallist.Add(2);
                return reallist;
            }
           else
            {
                reallist.Add(3);
                return reallist;
            }
        }
        public List<int> Guya_Uri()
        {
            List<int> reallist = new List<int>() { };
            for (int i = 0; i < AllVertex.Count; i++)
            {
                if(AllVertex[i].CountInPutVertex >= AllVertex.Count / 2 && AllVertex[i].CountOutPutVertex >= AllVertex.Count / 2)
                {
                    continue;
                }
                else
                {
                    reallist.Add(1);
                    reallist.Add(AllVertex[i]._CurrentNumber);
                    reallist.Add(AllVertex[i].CountInPutVertex);
                    reallist.Add(AllVertex[i].CountOutPutVertex);
                }
            }
            reallist.Add(2);
            return reallist;
        }
    }
}
