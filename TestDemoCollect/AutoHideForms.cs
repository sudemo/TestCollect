using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestDemoCollect
{
    public partial class AutoHideForms : Form
    {
        [DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

        // 下面是可用的常量，根据不同的动画效果声明自己需要的
        private const int AW_HOR_POSITIVE = 0x0001; // 自左向右显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        private const int AW_HOR_NEGATIVE = 0x0002; // 自右向左显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        private const int AW_VER_POSITIVE = 0x0004; // 自顶向下显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        private const int AW_VER_NEGATIVE = 0x0008; // 自下向上显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志该标志
        private const int AW_CENTER = 0x0010; // 若使用了AW_HIDE标志，则使窗口向内重叠；否则向外扩展
        private const int AW_HIDE = 0x10000; // 隐藏窗口
        private const int AW_ACTIVE = 0x20000; // 激活窗口，在使用了AW_HIDE标志后不要使用这个标志
        private const int AW_SLIDE = 0x40000; // 使用滑动类型动画效果，默认为滚动动画类型，当使用AW_CENTER标志时，这个标志就被忽略
        private const int AW_BLEND = 0x80000; // 使用淡入淡出效果
        public AutoHideForms()
        {
            InitializeComponent();
        }

        private void AutoHideForms_Load(object sender, EventArgs e)
        {

        }
        public void FMClosed()
        {
            int x = Screen.PrimaryScreen.WorkingArea.Right + this.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Bottom + this.Height;
            this.Location = new Point(1920, y); // 设置窗体在屏幕右下角显示
            AnimateWindow(this.Handle, 1000, AW_HOR_POSITIVE | AW_BLEND | AW_HIDE);

        }
    }




    /// <summary>
    /// 自动超时消息提示框
    /// </summary>
    public class MessageBoxTimeOut
    {
        /// <summary>
        /// 标题
        /// </summary>
        private static string _caption;

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="text">消息内容</param>
        /// <param name="caption">标题</param>
        /// <param name="timeout">超时时间，单位：毫秒</param>
        public static void Show(string text, string caption, int timeout)
        {
            _caption = caption;
            StartTimer(timeout);
            MessageBox.Show(text, caption);
        }
        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="text">消息内容</param>
        /// <param name="caption">标题</param>
        /// <param name="timeout">超时时间，单位：毫秒</param>
        /// <param name="buttons">消息框上的按钮</param>
        public static void Show(string text, string caption, int timeout, MessageBoxButtons buttons)
        {
            _caption = caption;
            StartTimer(timeout);
            MessageBox.Show(text, caption, buttons);
        }
        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="text">消息内容</param>
        /// <param name="caption">标题</param>
        /// <param name="timeout">超时时间，单位：毫秒</param>
        /// <param name="buttons">消息框上的按钮</param>
        /// <param name="icon">消息框上的图标</param>
        public static void Show(string text, string caption, int timeout, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            _caption = caption;
            StartTimer(timeout);
            MessageBox.Show(text, caption, buttons, icon);
        }
        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="owner">消息框所有者</param>
        /// <param name="text">消息内容</param>
        /// <param name="caption">标题</param>
        /// <param name="timeout">超时时间，单位：毫秒</param>
        public static void Show(IWin32Window owner, string text, string caption, int timeout)
        {
            _caption = caption;
            StartTimer(timeout);
            MessageBox.Show(owner, text, caption);
        }
        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="owner">消息框所有者</param>
        /// <param name="text">消息内容</param>
        /// <param name="caption">标题</param>
        /// <param name="timeout">超时时间，单位：毫秒</param>
        /// <param name="buttons">消息框上的按钮</param>
        public static void Show(IWin32Window owner, string text, string caption, int timeout, MessageBoxButtons buttons)
        {
            _caption = caption;
            StartTimer(timeout);
            MessageBox.Show(owner, text, caption, buttons);
        }
        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="owner">消息框所有者</param>
        /// <param name="text">消息内容</param>
        /// <param name="caption">标题</param>
        /// <param name="timeout">超时时间，单位：毫秒</param>
        /// <param name="buttons">消息框上的按钮</param>
        /// <param name="icon">消息框上的图标</param>
        public static void Show(IWin32Window owner, string text, string caption, int timeout, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            _caption = caption;
            StartTimer(timeout);
            MessageBox.Show(owner, text, caption, buttons, icon);
        }

        private static void StartTimer(int interval)
        {
            Timer timer = new Timer();
            timer.Interval = interval;
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Enabled = true;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            KillMessageBox();
            //停止计时器
            ((Timer)sender).Enabled = false;
        }

        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private extern static int PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private const int WM_CLOSE = 0x10;

        private static void KillMessageBox()
        {
            //查找MessageBox的弹出窗口,注意对应标题
            IntPtr ptr = FindWindow(null, _caption);
            if (ptr != IntPtr.Zero)
            {
                //查找到窗口则关闭
                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }
        }
    }
}



