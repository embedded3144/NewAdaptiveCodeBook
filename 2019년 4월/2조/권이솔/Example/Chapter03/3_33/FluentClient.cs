using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter03._3_33
{
    public interface IFluentInterface
    {
        IFluentInterface DoSomething();
        IFluentInterface DoSomethingElse();
        void ThisMethodIsNotFluent();
    }

    public class FluentImplementation : IFluentInterface
    {
        public IFluentInterface DoSomething()
        {
            return this;
        }

        public IFluentInterface DoSomethingElse()
        {
            return this;
        }

        public void ThisMethodIsNotFluent()
        {
        }
    }


    public class FluentClient
    {
        public FluentClient(IFluentInterface fluent)
        {
            this.fluent = fluent;
        }

        public void Run()
        {
            // 능동형 인터페이스를 사용하지 않는 경우
            fluent.DoSomething();
            fluent.DoSomethingElse();
            fluent.DoSomethingElse();
            fluent.DoSomething();
            fluent.ThisMethodIsNotFluent();

            // 능동형 인터페이스를 사용하는 경우
            fluent.DoSomething()
                .DoSomethingElse()
                .DoSomethingElse()
                .DoSomething()
                .ThisMethodIsNotFluent();
        }

        private readonly IFluentInterface fluent;
    }
}
