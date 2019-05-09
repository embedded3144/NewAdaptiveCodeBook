using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceExample
{
    public class SimpleInterfaceImplementation : ISimpleInterface
    {
        public void ThisMethodRequiresImplementation()
        {

        }

        public string ThisStringPropertyNeedsImplementingToo
        {
            get;
            set;
        }

        public int ThisIntegerPropertyOnlyNeedsAGetter
        {
            get
            {
                return this.encapsulatedInteger;
            }
            set
            {
                this.encapsulatedInteger = value;
            }
        }

        public event EventHandler<EventArgs> InterfaceCanContainEventsToo = delegate { };

        private int encapsulatedInteger;
    }
}
