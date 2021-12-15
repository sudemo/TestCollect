using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SixAsixesAnalyse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public double[] Forward(double Jx1, double Jy1, double Jz1, double JRolx1, double JRoly1, double JRolz1)
        {
            // 关节初始（绝对位置）
            double Jx0 = 0;
            double Jy0 = 0;
            double Jz0 = 0;
            double JRolx0 = 0;
            double JRoly0 = 0;
            double JRolz0 = 0;
            // 末端初double始（绝对位置），PI6轴与5轴只需要更改末端相对中心点坐标位置
            double x0 = 0;
            double y0 = 220.4;
            double z0 = 16.26;
            double Rolx0 = 0;
            double Roly0 = 0;
            double Rolz0 = 0;

            double dx = Jx1 - Jx0;      // mm // 关节实时相对位置，可修改
            double dy = Jy1 - Jy0;
            double dz = Jz1 - Jz0;
            double Roldx = JRolx1 - JRolx0;   // 弧度值，即rad; 其中1°=（pi / 180°）rad
            double Roldy = JRoly1 - JRoly0;
            double Roldz = JRolz1 - JRolz0;

            // r0 = sqrt(x0 ^ 2 + y0 ^ 2 + z0 ^ 2);
            // th1 = asin(y0 / x0);
            // th2 = atan(z0 / sqrt(x0 ^ 2 + y0 ^ 2));

            // RX旋转Roldx角度 （转轴公式）
            double x1 = x0;
            double y1 = y0 * Math.Cos(Roldx) - z0 * Math.Sin(Roldx);
            double z1 = z0 * Math.Cos(Roldx) + y0 * Math.Sin(Roldx);
            // RY旋转Roly角度 （转轴公式）
            double x2 = x1 * Math.Cos(Roldy) + z1 * Math.Sin(Roldy);
            double y2 = y1;
            double z2 = z1 * Math.Cos(Roldy) - x1 * Math.Sin(Roldy);
            // RZ旋转Roldz角度 （转轴公式）
            double x3 = x2 * Math.Cos(Roldz) - y2 * Math.Sin(Roldz);
            double y3 = y2 * Math.Cos(Roldz) + x2 * Math.Sin(Roldz);
            double z3 = z2;
            // DX,DY,DZ变化
            double x4 = x3 + dx;
            double y4 = y3 + dy;
            double z4 = z3 + dz;

            // 输出末端位置变化绝对位置
            double x10 = x4 - x0;
            double y10 = y4 - y0;
            double z10 = z4 - z0;
            double Rolx10 = JRolx1;
            double Roly10 = JRoly1;
            double Rolz10 = JRolz1;

            // r1 = sqrt(x1 ^ 2 + y1 ^ 2 + z1 ^ 2);
            // r2 = sqrt(x2 ^ 2 + y2 ^ 2 + z2 ^ 2);
            // r3 = sqrt(x3 ^ 2 + y3 ^ 2 + z3 ^ 2);
            // r4 = sqrt((x4 - dx) ^ 2 + (y4 - dy) ^ 2 + (z4 - dz) ^ 2);
            return new double[] { x10 ,
                                 y10  ,
                                 z10  ,
                                 Rolx10,
                                 Roly10,
                                 Rolz10};
        }
        public double[] Inverse(double X, double Y, double Z, double Rolx, double Roly, double Rolz)
        {
            // 关节初始（绝对位置）
            double Jx0 = 0;
            double Jy0 = 0;
            double Jz0 = 0;
            double JRolx0 = 0;
            double JRoly0 = 0;
            double JRolz0 = 0;
            // 末端初始（绝对位置），PI6轴和5轴只需要更改末端相对中心点坐标位置
            double x0 = 0;
            double y0 = 220.4;
            double z0 = 16.26;
            double Rolx0 = 0;
            double Roly0 = 0;
            double Rolz0 = 0;

            // 输出末端位置相对于中心点位置
            double x10 = X + x0;
            double y10 = Y + y0;
            double z10 = Z + z0;
            double Roldx = Rolx;
            double Roldy = Roly;
            double Roldz = Rolz;

            // RX旋转Roldx角度 （转轴公式）
            double x1 = x0;
            double y1 = y0 * Math.Cos(Roldx) - z0 * Math.Sin(Roldx);
            double z1 = z0 * Math.Cos(Roldx) + y0 * Math.Sin(Roldx);
            // RY旋转Roly角度 （转轴公式）
            double x2 = x1 * Math.Cos(Roldy) + z1 * Math.Sin(Roldy);
            double y2 = y1;
            double z2 = z1 * Math.Cos(Roldy) - x1 * Math.Sin(Roldy);
            // RZ旋转Roldz角度 （转轴公式）
            double x3 = x2 * Math.Cos(Roldz) - y2 * Math.Sin(Roldz);
            double y3 = y2 * Math.Cos(Roldz) + x2 * Math.Sin(Roldz);
            double z3 = z2;
            // DX,DY,DZ变化
            double Jx1 = x10 - x3;
            double Jy1 = y10 - y3;
            double Jz1 = z10 - z3;
            return new double[] { Jx1, Jy1, Jz1, Roldx, Roldy, Roldz };
        }

        public void MainConvert()
        {
            //   clc; clear; close;   // forward kinematics or  inverse kinematics
            // 关节实时绝对位置位置，可修改
            double Jx = 0.23;   // mm
            double Jy = -2.1;
            double Jz = -0.12;
            double JRolx = 0.43;   // 弧度值，即rad; 其中1°=（pi / 180°）rad
            double JRoly = -0.31;
            double JRolz = -0.132;
            // forward kinematics
            //[X, Y, Z, Rolx, Roly, Rolz] = forward(Jx, Jy, Jz, JRolx, JRoly, JRolz);
            var res = Forward(Jx, Jy, Jz, JRolx, JRoly, JRolz);
            double X = res[0];
            double Y = res[1];
            double Z = res[2];
            double Rolx = res[3];
            double Roly = res[4];
            double Rolz = res[5];
            double X1 = X;
            double Y1 = Y;
            double Z1 = Z;
            double Rolx1 = Rolx;
            double Roly1 = Roly;
            double Rolz1 = Rolz;
            // inverse kinematics;
            //[Jx1, Jy1, Jz1, JRolx1, JRoly1, JRolz1] = inverse(X1, Y1, Z1, Rolx1, Roly1, Rolz1);
            var res1 = Inverse(X1, Y1, Z1, Rolx1, Roly1, Rolz1);

            double x0 = 0;
            double y0 = 220.4;
            double z0 = 16.26;
            double r0 = Math.Sqrt(Math.Pow(x0 ,2) + Math.Pow(y0 ,2) + Math.Pow(z0 , 2));  // 初始坐标到中心点（0，0，0）位置距离
            double x10 = X + x0;     // 正解后转换到初始坐标的末端位置
            double y10 = Y + y0;
            double z10 = Z + z0;
            double r1 = Math.Sqrt(Math.Pow((X + x0 - Jx)+ (Y + y0 - Jy),2) + Math.Pow((Z + z0 - Jz) , 2)); // 正解后到初始坐标到中心点（0，0，0）位置距离
        }

        private void RunMtth_Click(object sender, EventArgs e)
        {
            MainConvert();
        }
    }
}
