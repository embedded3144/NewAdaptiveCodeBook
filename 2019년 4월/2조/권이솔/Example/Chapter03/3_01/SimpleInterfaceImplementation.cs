using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter03
{
    public class SimpleInterfaceImplementation : ISimpleInterface
    {
        public string ThisStringPropertyNeedsImplementingToo { get; set; }

        private int encapsultateInteger;
        public int ThisIntegerPropertyOnlyNeedsAGetter
        {
            get
            {
                return this.encapsultateInteger;
            }
            set
            {
                this.encapsultateInteger = value;
            }
        }

        public event EventHandler InterfaceCanContainEventsToo = delegate { };

        public void ThisMethodRequiresImplementation()
        {

        }
    }
}
