using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectDemo
{
    interface IReflectionInterfaceDemo
    {
        void MethodDemoA();

        void MethodDemoA(string para);
        void MethodDemoB(string para);

    }

    public class DemoClass : IReflectionInterfaceDemo
    {
        public void MethodDemoA()
        {
            Console.WriteLine("inside m");
        }

        public void MethodDemoA(string para)
        {
            Console.WriteLine("inside a");
        }

        public void MethodDemoB(string para)
        {
            Console.WriteLine("inside mb");
        }
    }
}
