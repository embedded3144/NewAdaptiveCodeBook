using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter03._3_29
{
    public interface ITargetInterface
    {
        void DoSomething();
    }

    public class TargetImplementation : ITargetInterface
    {
        public void DoSomething()
        {
            Console.WriteLine("ITargetInterface.DoSomething()");
        }
    }

    public interface IMixinInterfaceA
    {
        void MethodA();
    }

    public class MixinImplementaionA : IMixinInterfaceA
    {
        public void MethodA()
        {
            Console.WriteLine("IMixinInterfaceA.MethodA()");
        }
    }

    public interface IMixinInterfaceB
    {
        void MethodB(int parameter);
    }

    public class MixinImplementationB : IMixinInterfaceB
    {
        public void MethodB(int parameter)
        {
            Console.WriteLine("IMixinInterfaceB.MethodB({0})", parameter);
        }
    }

    public interface IMixinInterfaceC
    {
        void MethodC(string parameter);
    }

    public class MixinImplementationC : IMixinInterfaceC
    {
        public void MethodC(string parameter)
        {
            Console.WriteLine("IMixinInterfaceC.MethodC(\"{0}\")", parameter);
        }
    }
}
