using ORMDemo.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ORMDemo
{
    public partial class ORMForm1 : Form
    {
        public ORMForm1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            tableNameDAL t = new tableNameDAL();
            int i = 0;
            while (i < 100000)
            {

                string res = GetRandomString(5, false, true, false, false, "hello");
                if (t.AddUser(res, "paswd"))
                {
                    var rres = t.QueryUser(res);
                    richTextBox1.AppendText((string)rres[0].ToString() + i.ToString() + "\n");
                    i++;
                }
            }

        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //mySqlHelper.CreateTable();
            tableNameDAL t = new tableNameDAL();
            //t.DeleteUser("neo");
            t.DeleteAllUser();
        }
        ///生成随机字符串 
        ///</summary>
        ///<param name="length">目标字符串的长度</param>
        ///<param name="useNum">是否包含数字，1=包含，默认为包含</param>
        ///<param name="useLow">是否包含小写字母，1=包含，默认为包含</param>
        ///<param name="useUpp">是否包含大写字母，1=包含，默认为包含</param>
        ///<param name="useSpe">是否包含特殊字符，1=包含，默认为不包含</param>
        ///<param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
        ///<returns>指定长度的随机字符串</returns>
        public static string GetRandomString(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        {

            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom; //注意此处的逗号

            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }
            for (int i = 0; i < length; i++)
            {
                var n = r.Next(0, str.Length - 1);
                s += str.Substring(n, 1);
            }
            return s;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();

        }
        frmEx fx = new frmEx();
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                fx.TopLevel = false;     //设置为非顶级控件
                tabPage1.Controls.Add(fx);
                //tabControl1.TabPages.Add(tab);
                fx.Show();
            }
        }
    }
}

