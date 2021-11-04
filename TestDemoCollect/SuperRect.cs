using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RectTestClass
{
    public class SuperRect
    {
        Rectangle rectObj;
        static Graphics SG;
        public SuperRect(int _index, int[] _locationRC, int _locationX, int _locationY, string _text, PaintEventArgs ee)
        {
            this.Index = _index;
            this.LocationRC = _locationRC;
            this.Text = _text;
            rectObj = new Rectangle();
            rectObj.Width = this.Width;
            rectObj.Height = this.Height;
            this.X = _locationX;
            this.Y = _locationY;
            SG =ee.Graphics;
            //SG.FillRectangle(new Pen(Color.Red),X,Y,Width,Height);
            //SG.FillRectangle();
        }
      
        public int Width { get; set; } = 50;
        public int Height { get; set; } = 50;

        /// <summary>
        /// 索引号，从0 开始
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 行列编号r,c;r*c = index
        /// </summary>
        public int[] LocationRC { get; set; }
        
        /// <summary>
        /// 框里的字符串
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 方框在界面的坐标
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// 方框在界面的坐标
        /// </summary>
        public int Y { get; set; }

        public Color RectColor { get; set; } = Color.Green;
        public bool isNeedRun { get; set; } = false;
       /// <summary>
       /// 被选中的 作为运动路径的运动顺序，是否开启，严格按照点选顺序作为运动顺序，还是按照默认的index顺序呢
       /// </summary>
        public int RunOrder { get; set; }

        private bool isSelected { get; set; } = false;

        /// <summary>
        /// 当前rect所代表的 轴的实际位置,此处单位可能为脉冲值或长度单位
        /// </summary>
        public int[] MotionCoordinate { get; set; } = { 0, 0 };


        /// <summary>
        /// 是否被选中，select,contains
        /// </summary>
        public bool GetIsSelected()
        {
            return isSelected;
        }

        /// <summary>
        /// 是否被选中，select,contains
        /// </summary>
        public void SetIsSelected(bool value)
        {
            isSelected = value;
        }

        //public delegate void SuperRectEvent(object sender, EventArgs e);

        //public event SuperRectEvent SuperRectClickedEvent;
        //public event SuperRectEvent SuperRectSelectedEvent;//事件
        //private void SuperRect_Clicked(object sender, EventArgs e) //事件触发方法
        //{
        //    SuperRectClickedEvent(sender,e);
        //}
        //public void SuperRect_Selected(object sender,EventArgs e) //事件触发方法
        //{
        //    SuperRectSelectedEvent(sender, e);
        //}
        ////方法

        public void DrawSuperRect()
        {
            SG.FillRectangle(new SolidBrush(RectColor), X, Y, Width, Height);
            if (Text != "" || Text != null)
            {
                //new Font(this.Font, FontStyle.Bold); this.Font = new Font(this.Font, FontStyle.Bold);
                SG.DrawString(Text, new Font("arial", 16), new SolidBrush(Color.White), X-5, Y+Height-22);//左下角
            }
        }
        public void DrawSuperRect(Brush solidBrush)
        {
            SG.FillRectangle(solidBrush, X, Y, Width, Height);
            if (Text != "" || Text != null)
            {
                //new Font(this.Font, FontStyle.Bold); this.Font = new Font(this.Font, FontStyle.Bold);
                SG.DrawString(Text, new Font("arial", 16), new SolidBrush(Color.Red), X, Y);
            }
        }

        public void ChangeColor(Color para)
        {
            this.RectColor = para;
            SG.FillRectangle(new SolidBrush(RectColor),X, Y, Width, Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Containsp(int x,int y) 
        {

            Console.WriteLine("rect x:{0},y:{1}",this.X,this.Y);
          
            if ((this.X + Width) > x && (this.Y + Height) > y)
            {

                Console.WriteLine("input x:{0},y:{1}", x, y);
                return true;
            }
            else { return false; }
        }
        public bool Contains(int x, int y) => X <= x && x < X + Width && Y <= y && y < Y + Height;
    }
}
