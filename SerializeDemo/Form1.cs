using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SerializeDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DemoMeth();
        }
        public void DemoMeth()
        {
            MyObject obj = new MyObject();

            obj.n1 = 1;

            obj.n2 = 24;

            obj.str = "一些字符串";
            //obj.DmT();

            //二进制序列化
            IFormatter formatter = new BinaryFormatter();

            Stream stream = new FileStream("MyFile.txt", FileMode.Create,

            FileAccess.Write, FileShare.None);

            formatter.Serialize(stream, obj);

            stream.Close();

            //序列化xml格式
            //XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
            ////MemoryStream stream = new MemoryStream();
            //Stream stream = new FileStream("MyFile.txt", FileMode.Create,

            //FileAccess.Write, FileShare.None);
            //xmlSerializer.Serialize(stream, obj);
            ////byte[] buf = stream.ToArray();
            ////string xml = Encoding.ASCII.GetString(buf);
            //stream.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IFormatter formatter = new BinaryFormatter();

            Stream stream = new FileStream("MyFile.txt", FileMode.OpenOrCreate,

            FileAccess.Read, FileShare.None);

            //formatter.Serialize(stream, obj);
            //IFormatter formatter = new BinaryFormatter();
            //反序列化后强制转换
            MyObject dobj = (MyObject)formatter.Deserialize(stream);
            dobj.DmT();
            stream.Close();
        }
    }

    [Serializable]

    public class MyObject

    {
        public int n1 = 0;
        public int n2 = 0;
        public String str = null;
    
        RefDemo RefDemo = new RefDemo();

        public void DmT()
        {
            RefDemo.ChangeValue(123);
        }
    }
    [Serializable]
    public class RefDemo
    {
        public int n11 = 0;
        public int n22 = 0;
        public String str1 = null;
        public void ChangeValue(int v)
        {
            n22 = v;
        }
    }
}
