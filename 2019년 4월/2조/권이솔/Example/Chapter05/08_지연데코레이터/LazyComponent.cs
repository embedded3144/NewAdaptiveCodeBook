using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter05._8_지연데코레이터
{
    public class LazyComponent : IComponant
    {
        private readonly Lazy<IComponant> lazyComponent;

        public LazyComponent(Lazy<IComponant> lazyComponent)
        {
            this.lazyComponent = lazyComponent;
        }

        public void Something()
        {
            lazyComponent.Value.Something();
        }
    }
}
