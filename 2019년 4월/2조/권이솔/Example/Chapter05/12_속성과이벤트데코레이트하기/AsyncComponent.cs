using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter05._12_속성과이벤트데코레이트하기
{
    public class AsyncComponent : IComponant
    {
        private readonly IComponant decoratedComponent;

        public AsyncComponent(IComponant decoratedComponent)
        {
            this.decoratedComponent = decoratedComponent;
        }

        public string Property
        {
            get
            {
                // 데코레이트된 속성 값을 조회한 후 리턴 전에 변경할 수도 있다.
                return decoratedComponent.Property;
            }
            set
            {
                // 데코레이트된 속성에 대입할 값을 변경할 수도 있다.
                decoratedComponent.Property = value;
            }
        }

        public event EventHandler Event;

        public void Process()
        {
            throw new NotImplementedException();
        }
    }
}
