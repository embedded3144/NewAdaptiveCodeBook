using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter05._8_지연데코레이터
{
    public class ComponentClient
    {
        private readonly IComponant component;

        public ComponentClient(IComponant component)
        {
            this.component = component;
        }

        public void Run()
        {
            component.Something();
        }
    }
}
