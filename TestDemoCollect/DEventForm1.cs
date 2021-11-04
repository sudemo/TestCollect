using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestDemoCollect
{
    public partial class DEventForm1 : Form
    {
       
        public DEventForm1()
        {
            InitializeComponent();

        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            MyEvent Evt = new MyEvent();//实例化
            Evt.OnInput += On_Input; //绑定事件到方法  
            Evt.WaitInput();
        }
        private void On_Input(object sender, EventArgs e)
        {
           
            Console.WriteLine("你触发了‘X’！");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 浏览器的开启后，控制台的界面依然保持
            // 在运行程序之前，谷歌浏览器已经开启。
            // 程序的运行效果是：追加标签页，而不是新开一个程序。
            string url = @"D:\CODING\GitRespository\pythonweb\cuiqingcai.com\templates\message.html";
            //string path = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            //Process.Start(path,
            //    url);
            //使用默认浏览器打开
            Process.Start(url);
        }

        private void button3_Click(object sender, EventArgs e)
        {
#if DEBUG
            MessageBox.Show("Test");
#endif
            Label lb = new Label();
            lb.Text = "hello";
            lb.Visible = true;
            lb.Size = new System.Drawing.Size(47, 15);
            lb.Location = new System.Drawing.Point(428, 92);
            lb.Parent = this;
            this.Controls.Add(lb);
            lb.Show();
            //AnimateWindow(MessageBox.Handle, 1000, AW_BLEND | AW_HIDE);
            NewBtn();
            AutoHideForms autoHideForms = new AutoHideForms();
            autoHideForms.Show();
            //Thread.Sleep(10000);
            //autoHideForms.FMClosed();
            //autoHideForms.Close();
            MessageBoxTimeOut.Show("你好，我是超时消息框，3秒后自动关闭！", "提示", 1000);

        }

        private void NewBtn()
        {
            Button button5 = new Button();
            button5.Location = new System.Drawing.Point(128, 209);
            button5.Name = "button3";
            button5.Size = new System.Drawing.Size(84, 52);
            button5.TabIndex = 2;
            button5.Text = "弹窗测试";
            button5.UseVisualStyleBackColor = true;
            button5.Parent = this;
            this.Controls.Add(button5);
            //button5.Click += new System.EventHandler(this.button3_Click);
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
    class MyEvent //声明事件
    {
        public event EventHandler<EventArgs> OnInput; //定义一个委托类型的事件  
        public void WaitInput()
        {
            //while (true)
            //{
            //    if (Console.ReadLine() == "x")
            //        OnInput(this, new EventArgs()); //触发事件
            //}
            
            OnInput(this, new EventArgs()); //触发事件
        }
    }
}
