using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter05._6_조건부데코레이터
{
    class PredicatedDecoratorExample
    {
        private readonly IComponent component;
        public PredicatedDecoratorExample(IComponent component)
        {
            this.component = component;
        }

        public void Run()
        {
            component.Something();
        }
    }
}
