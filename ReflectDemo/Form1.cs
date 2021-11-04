using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace ReflectDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProgramDemo.MainEntry("s");
        }
    }


    public class Test
    {
        // 无参数，无返回值方法
        public void Method()
        {
            Console.WriteLine("Method（无参数） 调用成功！");
        }

        // 有参数，无返回值方法
        public void Method(string str)
        {
            Console.WriteLine("Method（有参数） 调用成功！参数 ：" + str);
        }

        // 有参数，有返回值方法
        public string Method(string str1, string str2)
        {
            Console.WriteLine("Method（有参数，有返回值） 调用成功！参数 ：" + str1 + ", " + str2);
            string className = this.GetType().FullName;         // 非静态方法获取类名
            return className;
        }
    }

    static class ProgramDemo
    {
        public static void MainEntry(string args)
        {
            string strClass = "ReflectDemo.Test";           // 命名空间+类名
            string strMethod = "Method";        // 方法名

            Type type;                          // 存储类
            Object obj;                         // 存储类的实例

            type = Type.GetType(strClass);      // 通过类名获取同名类
            obj = Activator.CreateInstance(type);       // 创建实例

            MethodInfo method = type.GetMethod(strMethod, new Type[] { });      // 获取方法信息
            object[] parameters = null;
            method.Invoke(obj, parameters);                           // 调用方法，参数为空

            // 注意获取重载方法，需要指定参数类型
            method = type.GetMethod(strMethod, new Type[] { typeof(string) });      // 获取方法信息
            parameters = new object[] { "hello" };
            method.Invoke(obj, parameters);                             // 调用方法，有参数

            method = type.GetMethod(strMethod, new Type[] { typeof(string), typeof(string) });      // 获取方法信息
            parameters = new object[] { "hello", "你好" };
            string result = (string)method.Invoke(obj, parameters);     // 调用方法，有参数，有返回值
            Console.WriteLine("Method 返回值：" + result);                // 输出返回值

            // 获取静态方法类名
            string className = MethodBase.GetCurrentMethod().ReflectedType.FullName;
            Console.WriteLine("当前静态方法类名：" + className);

            Console.ReadKey();

        }
        public static void GetMethodByName(string strMethodName)
        {
            string strClass = "ReflectDemo.Test";           // 命名空间+类名
            //string strMethod = "Method";        // 方法名

            Type type;                          // 存储类
            Object obj;                         // 存储类的实例

            type = Type.GetType(strClass);      // 通过类名获取同名类
            obj = Activator.CreateInstance(type);       // 创建实例

            MethodInfo method = type.GetMethod(strMethodName, new Type[] { });      // 获取方法信息
            object[] parameters = null;
            method.Invoke(obj, parameters);                           // 调用方法，参数为空

            //// 注意获取重载方法，需要指定参数类型
            method = type.GetMethod(strMethodName, new Type[] { typeof(string) });      // 获取方法信息
            parameters = new object[] { "hello" };
            method.Invoke(obj, parameters);
            Console.WriteLine("end");
            Console.ReadKey();
        }
    }
    public class DemoClassAA
    {
        public void Meth()
        {
            Console.WriteLine("ss");
            Console.ReadKey();

        }
    }
}
