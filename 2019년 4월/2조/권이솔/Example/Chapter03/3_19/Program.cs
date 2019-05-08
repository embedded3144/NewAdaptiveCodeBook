using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter03._3_19
{
    public interface IExpectedInterface
    {
        void MethodA();
    }

    public class Adapter : IExpectedInterface
    {
        private TargetClass target;

        public Adapter(TargetClass target)
        {
            this.target = target;
        }

        public void MethodA()
        {
            target.MethodB();
        }
    }

    public class TargetClass
    {
        public void MethodB()
        {

        }
    }

    class Program
    {
        static IExpectedInterface dependency = new Adapter(new TargetClass());
        static void Main(string[] args)
        {
            dependency.MethodA();
        }
    }
}
