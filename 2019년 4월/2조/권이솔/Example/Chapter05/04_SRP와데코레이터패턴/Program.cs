using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter05._4_SRP와데코레이터패턴
{
    public interface IComponent
    {
        void Something();
    }


    public class ConcreteComponent : IComponent
    {
        public void Something()
        {

        }
    }

    public class DecoratorComponent : IComponent
    {
        public DecoratorComponent(IComponent decoratedComponent)
        {
            this.decoratedComponent = decoratedComponent;
        }

        public void Something()
        {
            SomethingElse();
            decoratedComponent.Something();
        }

        private void SomethingElse()
        {
        }

        private readonly IComponent decoratedComponent;
    }

    class Program
    {
        static IComponent component;

        static void Main(string[] args)
        {
            component = new DecoratorComponent(new ConcreteComponent());
            component.Something();
        }
    }
}
