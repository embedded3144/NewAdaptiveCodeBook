using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter03
{
    public class ExplicitInterfaceImplementation : ISimpleInterface
    {
        public ExplicitInterfaceImplementation()
        {
            this.encapsultateInteger = 4;
        }

        public string ThisStringPropertyNeedsImplementingToo { get; set; }

        
        public int ThisIntegerPropertyOnlyNeedsAGetter
        {
            get
            {
                return this.encapsultateInteger;
            }
        }

        event EventHandler<EventArgs> InterfaceCanContainEventsToo
        {
            add { encapsulatedEvent += value; }
            remove { encapsulatedEvent -= value; }
        }

        event EventHandler ISimpleInterface.InterfaceCanContainEventsToo
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        public void ThisMethodRequiresImplementation()
        {
            encapsulatedEvent(this, EventArgs.Empty);
        }

        private int encapsultateInteger;
        private event EventHandler<EventArgs> encapsulatedEvent;
    }
}
