using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class clustercs
    {
        static int cluster_number;
        minuim__spaining mst_tree;
        int[] visted;
        int[] removed_node;
        double segma = 0;
        double last_segma = 0;
        int node = 0;
        int cluster_count = 0;
        int temp = 1;
        Tuple<int,int,int>[] clust_arr;
        int vertices = 0;
        int red = 0;
        int green = 0;
        int blue = 0;
        public clustercs(minuim__spaining tree , int cluster , int size )
        {
           
            cluster_number = cluster;
            mst_tree = tree;
            removed_node = new int [2*cluster_number];
            clust_arr = new Tuple<int,int,int>[cluster_number];
            visted = new int[size];
            vertices = size;
           
            for(int i = 0; i < size; i++)
            {
                if(i < cluster_number)
                {
                    clust_arr[i] = new Tuple<int, int, int>(0, 0, 0);
                }
                visted[i] = -1;
            }
            
           
        }

        public void constrct_clust()
        {

            Tuple<double, int, int> frist_max = new Tuple<double, int, int>(0, 0, 0);
            Tuple<double, int, int> edge = new Tuple<double, int, int>(0,0,0);
            int count = 1;
            mst_tree.edges.Sort();
            while(count != cluster_number)
            { 
                int index = mst_tree.edges.Count()-1;
                int temp = index - 1;
                frist_max = mst_tree.edges[index];
                edge = mst_tree.edges[temp];
                if(edge.Item1 != frist_max.Item1 )
                {
                    frist_max = mst_tree.edges[index];
                }
                else
                while (edge.Item1  == frist_max.Item1)
                {
                    frist_max = mst_tree.edges[index];
                    edge = mst_tree.edges[temp];
                    int node1 = frist_max.Item2;
                    int node2 = frist_max.Item3;
                        int node3 = edge.Item2;
                        int node4 = edge.Item3;
                        int cnode1 = mst_tree.mst[node1].Count();
                        int cnode2 = mst_tree.mst[node2].Count();
                        int cnode3 = mst_tree.mst[node3].Count();
                        int cnode4 = mst_tree.mst[node4].Count();
                        int min1 = Math.Min(cnode1, cnode2);
                        int max1 = Math.Min(cnode1, cnode2);
                        int min2 = Math.Min(cnode3, cnode4);
                        int max2 = Math.Min(cnode3, cnode4);
                        if (min1 >= max2)
                        {
                            frist_max = new Tuple<double, int, int>(frist_max.Item1, node1, node2);
                            temp--;
                        }
                        else if (min2 >= max1)
                        {
                            frist_max = edge;
                            index = temp;
                             temp--;
                        }
                        else if (min1 >= min2)
                        {
                            frist_max = new Tuple<double, int, int>(frist_max.Item1, node1, node2);
                            temp--;
                        }
                        else
                        {
                            frist_max = edge;
                            index = temp;
                             temp--;
                    }
                        if(temp == -1)
                        {
                            break;
                        }
                }

                mst_tree.edges.Remove(frist_max);
                int parent = frist_max.Item2;
                int child =  frist_max.Item3;
                removed_node[node] = parent;
                node++;
                removed_node[node] = child;
                node++;
                mst_tree.mst[parent].Remove(child);
                mst_tree.mst[child].Remove(parent);
                count++;
                frist_max = new Tuple<double, int, int>(0, 0, 0);
            }
            
            for(int i = 0; i < node ;i++)
            {
                temp = 0;
                if (visted[removed_node[i]] == -1)
                {
                    dfs(removed_node[i]);
                    red = red / temp;
                    green = green / temp;
                    blue = blue / temp;
                    clust_arr[cluster_count] = new Tuple<int, int, int>(red, green, blue);
                    cluster_count++;
                    red = 0;
                    blue = 0;
                    green = 0;
                    
                }
            }
        }
        void dfs(int s)
        {
            
            if(visted[s] != -1)
            {
                return;
            }
            visted[s] = cluster_count;
            Tuple<int, int, int> color;
            color = mst_tree.colors[s];
            red += color.Item1;
            green += color.Item2;
            blue += color.Item3;
            temp++;
            List<int> items = new List<int>();
            items = mst_tree.mst[s];
            for (int i = 0; i < mst_tree.mst[s].Count; i++)
            {
                if (visted[items[i]] == -1)
                {
                    
                    dfs(items[i]);

                }
            }

        }

        public RGBPixel[,] Replace(RGBPixel[,] imagematrix , int width , int hight , int[,,] index )
        {
            
            for(int i = 0; i <hight;i++)
            {
                for(int j = 0; j < width; j++)
                {
                    RGBPixel temp = imagematrix[i, j];
                    Tuple<int, int, int> repsentive_color;
                    int postion;
                    int k;
                    postion = index[temp.red, temp.green, temp.blue];
                    k = visted[postion];
                    repsentive_color = clust_arr[k];
                    imagematrix[i, j].red = Convert.ToByte(repsentive_color.Item1);
                    imagematrix[i, j].green = Convert.ToByte(repsentive_color.Item2);
                    imagematrix[i, j].blue = Convert.ToByte(repsentive_color.Item3);  
                }
            }

            return imagematrix;
        }


        public int automatic_cluster(minuim__spaining tree)
        {
            double sub = 1;
            double avg;
            int kluster = 1;
            while (sub >= 0.0001 && kluster <= mst_tree.bonus.Count)
            {
                avg = tree.bonus.Average();
                segma = 0;
                foreach (double i in tree.bonus)
                {
                    double temp = i - avg;
                    temp = temp * temp;
                    segma += temp;
                }
                segma = segma / avg;
                segma = Math.Sqrt(segma);
                double val1 = Math.Abs(segma - tree.bonus.Max);
                double val2 = Math.Abs(segma - tree.bonus.Min);
                double max = tree.bonus.Max;
                double min = tree.bonus.Min;
                if (val1 > val2)
                {
                    tree.bonus.Remove(max);
                }
                else
                {
                    tree.bonus.Remove(min);
                }

                sub = Math.Abs(segma - last_segma);
                last_segma = sub;
                kluster++;
            }

            return kluster;
        }
     
    }
}
