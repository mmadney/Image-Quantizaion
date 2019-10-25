using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Priority_Queue;

namespace ImageQuantization
{
    public class minuim__spaining
    {
        static bool[] marked;
        public Tuple<int, int, int>[] colors;
        Tuple<double, int>[] best_cost;
        public List<Tuple<double, int, int>> edges;
      //  public List<Tuple<double , int , int >> costs;
        public SortedSet<double> bonus;
        public List<int>[] mst;
        public   int vertice;
        public double minumcost = 0;
        public int kluster;
        public int index = 0;
        public minuim__spaining(List<RGBPixel> param , int klust)
        {

            int size = param.Count;
            kluster = klust;
            marked = new bool[size];
            bonus = new SortedSet<double>();
            edges = new List<Tuple<double, int, int>>();
           // costs = new List<Tuple<double , int, int>>();
            mst = new List<int>[size];
            colors = new Tuple<int, int, int>[size];
            best_cost = new Tuple<double, int>[size];
            vertice = size;
            for(int i = 0; i < size; i++)
            {
                best_cost[i] = new Tuple<double, int>(1000000, -1);
                mst[i] = new List<int>();
                colors[i] = (new Tuple<int, int, int>(Convert.ToInt32(param[i].red), Convert.ToInt32(param[i].green), Convert.ToInt32(param[i].blue)));
            }
                  
            
        }

        double cal_weight(Tuple<int, int, int> x, Tuple<int, int, int> y)
        {
            double Euclidean = 0;

            int red = x.Item1 - y.Item1;
            int green = x.Item2 - y.Item2;
            int blue = x.Item3 - y.Item3;
            Euclidean = (red * red) + (green * green) + (blue * blue);
            Euclidean = Math.Sqrt(Euclidean);
            return Euclidean;
        }

        public  void prim(int x)
        {

            int current = 0;
            int parent = 0;
           
            double cost = 0;

            while (true)
            {
                int childindex = -1;
                double min = 1000000;

                marked[current] = true;
                for (int j = 0; j < vertice; j++)
                {
                    if (marked[j] == true)
                    {
                        continue;
                    }
                    cost = cal_weight(colors[current], colors[j]);
                    double last_best = best_cost[j].Item1;

                    if (cost < last_best)
                    {
                        best_cost[j] = new Tuple<double, int>(cost, current);
                    }
                    double best = best_cost[j].Item1;
                    if (best <= min)
                    {
                        parent = best_cost[j].Item2;
                        childindex = j;
                        min = best;

                    }
                }
                if (childindex == -1)
                {
                    break;
                }

                minumcost += min;
                edges.Add(new Tuple<double, int, int>(min, parent, childindex));
                bonus.Add(min);
                mst[parent].Add(childindex);
                mst[childindex].Add(parent); 
               current = childindex;

            }



        }

    }
}