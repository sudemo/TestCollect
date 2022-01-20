using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReflectDemo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            ReflectDemo.ProgramDemo.GetMethodByName("Method");
            DemoClassAA demo = new DemoClassAA();
            demo.Meth();
            Program3.GenMain("s");
        }
    }
    public class GenericClass<T>
    {
        public T varT;
    }
    class Program2
    {
        /// <summary>
        /// 泛型类
        /// </summary>
        /// <typeparam name="T"></typeparam>
       

        static void GenMain(string[] args)
        {
            #region 泛型类
            //T是int类型
            GenericClass<int> genericInt = new GenericClass<int>
            {
                varT = 123
            };
            Console.WriteLine($"The value of T={genericInt.varT}");
            //T是string类型
            GenericClass<string> genericString = new GenericClass<string>
            {
                varT = "123"
            };
            Console.WriteLine($"The value of T={genericString.varT}");
            Console.Read();
            #endregion
        }
    }
    class Program3
    {
        /// <summary>
        /// 泛型委托  如果一个委托要传递两种对象，只需要再构造一个泛型类，泛型委托的时候传入这个泛型类，泛型类种指定对象类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public delegate void SayHi<T>(T t);

        public static void GenMain(string args)
        {
            Program2 program2 = new Program2();
            GenericClass<object> genObj = new GenericClass<object>();
            #region 泛型委托
            SayHi<string> sayHi = SayHello;
            sayHi("Hello World");
            //泛型类
            SayHi<GenericClass<object>> sayHi1 = SayHello;
            sayHi1(genObj);
            Console.Read();
            #endregion
        }

        /// <summary>
        /// SayHello
        /// </summary>
        /// <param name="greeting"></param>
        public static void SayHello(string greeting)
        {
            Console.WriteLine($"{greeting}");
        }
        public static void SayHello(GenericClass<object> greeting)
        {
            Console.WriteLine($"{greeting}");
        }
    }
}

