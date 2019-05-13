using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter05._6_조건부데코레이터
{
    public class PredicatedComponent : IComponent
    {
        private readonly IComponent decoratedComponent;
        private readonly IPredicate predicate;

        public PredicatedComponent(IComponent decoratedComponent, IPredicate predicate)
        {
            this.decoratedComponent = decoratedComponent;
            this.predicate = predicate;
        }

        public void Something()
        {
            if (predicate.Test())
                decoratedComponent.Something();
        }
    }

    
}
