using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class Graph
    {
        public int[,,] size = new int[256, 256, 256];
        public  List<RGBPixel> distinct_arry(RGBPixel[,] ImageMatrix , int width , int height)
        {
            int count = 0;
            List<RGBPixel> Distinct = new List<RGBPixel> (width*height);
            for(int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    for (int z = 0; z <256 ; z++)
                    {
                        size[i, j, z] = -1;
                    }
                }
            }  
            for (int i=0;i < height;i++)
            {
                for(int j =0; j < width ; j++)

                {
                    int red = ImageMatrix[i, j].red;
                    int green = ImageMatrix[i, j].green;
                    int blue= ImageMatrix[i, j].blue;
                    if (size[red, green ,blue] == -1)
                    {
                        Distinct.Add(ImageMatrix[i, j]);
                        size[red, green, blue] = count;
                        count++;
                    }  
                }
            }

            return Distinct;

        }

       

    }
}
