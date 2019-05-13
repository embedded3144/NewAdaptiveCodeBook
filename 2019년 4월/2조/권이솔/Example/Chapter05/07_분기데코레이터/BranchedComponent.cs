using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter05._7_분기데코레이터
{
    class BranchedComponent : IComponent
    {
        private readonly IComponent trueComponent;
        private readonly IComponent falseComponent;
        private readonly IPredicate predicate;

        public BranchedComponent(IComponent trueComponent, IComponent falseComponent, IPredicate predicate)
        {
            this.trueComponent = trueComponent;
            this.falseComponent = falseComponent;
            this.predicate = predicate;
        }

        public void Something()
        {
            if (predicate.Test())
                trueComponent.Something();
            else
                falseComponent.Something();
        }
    }
}
