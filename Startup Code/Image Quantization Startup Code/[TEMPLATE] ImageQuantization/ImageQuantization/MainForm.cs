using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace ImageQuantization
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

        }

        RGBPixel[,] ImageMatrix;
        clustercs k;
        static public Tuple<int, int, int>[] colors;
        int width = 0;
        int hight = 0;
        minuim__spaining tree;
     

        private void btnOpen_Click(object sender, EventArgs e)
        {
            
           
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }
            txtWidth.Text = ImageOperations.GetWidth(ImageMatrix).ToString();
            txtHeight.Text = ImageOperations.GetHeight(ImageMatrix).ToString();
             width = ImageOperations.GetWidth(ImageMatrix);
             hight = ImageOperations.GetHeight(ImageMatrix);
        }

        private void btnGaussSmooth_Click(object sender, EventArgs e)
        {
            
            Graph g = new Graph();
            int num_clust = 0;
            List<RGBPixel> Distinct = new  List<RGBPixel> (width * hight);
            num_clust = Convert.ToInt32(textBox1.Text);
            Distinct = g.distinct_arry(ImageMatrix, width, hight);
            textBox3.Text= Distinct.Count.ToString();
            tree = new minuim__spaining(Distinct , num_clust);
            tree.prim(0);
            textBox2.Text = tree.minumcost.ToString();
           
            k = new clustercs(tree , num_clust , Distinct.Count);
            k.constrct_clust();
            ImageMatrix = k.Replace(ImageMatrix, width, hight , g.size);
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int kluster = 0;
            try
            {
                kluster = k.automatic_cluster(tree);
                MessageBox.Show("number of k", kluster.ToString());
            }
            catch
            {
                MessageBox.Show("please quntization the image first");
            }

           
        }
    }
}