using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCVDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Window srcWind;
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        //public const string Image1_File = @"C:\Images\1.PNG";
        public const string Image1_File = @"C:\Users\zwzhang\Pictures\Image_20210723174712700.bmp";
        //string img3= @"‪C:\Users\zwzhang\Pictures\Snipaste_2021-02-04_18-32-32.png"; //；路径错误，格式不对,冒号格式不对
              
        string img2 = @"D:\CODING\GitRespository\OpenCV-Python-Tutorial\data\aloeGT.png";//正确
        private void Met()
        {
            //pictureBox1.Image= Image.FromFile(img2);
            var img = Cv2.ImRead(img2,ImreadModes.AnyDepth);
            Mat image1 = new Mat(img2, ImreadModes.AnyColor);
            Mat image2 = new Mat(img2,ImreadModes.Grayscale);
            Mat mat3 = new Mat();
            //mat3.Create(image1.Cols - image2.Cols + 1, image1.Rows - image2.Rows + 1, MatType.CV_32FC1);
            //Cv2.MatchTemplate(image1, image2, mat3, TemplateMatchModes.SqDiff);
            //Cv2.Normalize(mat3, mat3, 1, 0, NormTypes.MinMax, -1);
            //Cv2.ImShow("1", mat3);
            using (new Window("src image", img))           
            {
                
                Cv2.WaitKey();
            }
        }
        
        private void Met2()
        {
            var img = Cv2.ImRead(img2, ImreadModes.Grayscale);
            pictureBox1.Width = img.Width/2;
            pictureBox1.Height = img.Height/2;
            Mat mat3 = new Mat(new OpenCvSharp.Size(img.Width,img.Height),MatType.CV_8UC1);
            Cv2.ApplyColorMap(img,mat3,ColormapTypes.Hsv);
            Bitmap res= BitmapConverter.ToBitmap(img); //mat转bitmap
            //res.Save("res");
            pictureBox1.Image = res;

            //srcWind = new Window("src image", mat3);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Met2();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.A)
            {
                srcWind.Close();
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            {
                srcWind.Close();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

            //{
            //    srcWind.Close();
            //} 
        }

        //private void tabPage1_Click(object sender, EventArgs e)
        //{

        //}
    }
    class Program1
    {

    }
}
