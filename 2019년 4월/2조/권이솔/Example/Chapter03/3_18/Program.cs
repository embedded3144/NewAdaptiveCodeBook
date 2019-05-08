using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter03._3_18
{
    public class Adaptee
    {
        public void MethodB()
        {

        }
    }

    public class Adapter : Adaptee
    {
        public void MethodA()
        {
            MethodB();
        }
    }

    class Program
    {
        static Adapter dependency = new Adapter();
        static void Main(string[] args)
        {
            dependency.MethodA();
        }
    }
}
