using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter03._3_20
{
    public interface IStrategy
    {
        void Execute();
    }

    public class ConcreteStrategyA : IStrategy
    {
        public void Execute()
        {
            Console.WriteLine("ConcreteStrategyA.Execute()");
        }
    }

    public class ConcreteStrategyB : IStrategy
    {
        public void Execute()
        {
            Console.WriteLine("ConcreteStrategyB.Execute()");
        }
    }

    public class Context
    {
        public Context()
        {
            currentStrategy = strategyA;
        }

        public void DoSomeThing()
        {
            currentStrategy.Execute();

            // 메서드를 호줄할 때마다 전략 객체를 변경한다.
            currentStrategy = (currentStrategy == strategyA) ? strategyB : strategyA;

        }

        private readonly IStrategy strategyA = new ConcreteStrategyA();
        private readonly IStrategy strategyB = new ConcreteStrategyB();

        private IStrategy currentStrategy;
    }


}
