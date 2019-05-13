using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter05._12_속성과이벤트데코레이트하기
{
    public class ComponentDecorator : IComponant
    {
        private readonly IComponant decoratedComponant;

        public ComponentDecorator(IComponant decoratedComponant)
        {
            this.decoratedComponant = decoratedComponant;
        }

        public ComponentDecorator()
        {
        }

        public event EventHandler Event
        {
            add
            {
                // 이벤트를 등록하기 전에 필요한 작업을 수행할 수도 있다.
                decoratedComponant.Event += value;
            }
            remove
            {
                // 이벤트를 분리해 내기 전에 필요한 작업을 수행할 수도 있다.
                decoratedComponant.Event -= value;
            }
        }

        public string Property { get; set; }
    }
}
