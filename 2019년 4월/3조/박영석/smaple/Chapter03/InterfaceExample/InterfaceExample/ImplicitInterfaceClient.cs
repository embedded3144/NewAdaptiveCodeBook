using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceExample
{
    public class ImplicitInterfaceClient
    {
        public ImplicitInterfaceClient(SimpleInterfaceImplementation implementationReference, ISimpleInterface interfaceReference)
        {
            var instancePropertyValue = implementationReference.ThisIntegerPropertyOnlyNeedsAGetter;
            implementationReference.ThisMethodRequiresImplementation();
            implementationReference.ThisStringPropertyNeedsImplementingToo = "Hello";
            implementationReference.InterfaceCanContainEventsToo += EventHandler;

            var interfacePropertyValue = interfaceReference.ThisIntegerPropertyOnlyNeedsAGetter;
            interfaceReference.ThisMethodRequiresImplementation();
            interfaceReference.ThisStringPropertyNeedsImplementingToo = "Hello";
            interfaceReference.InterfaceCanContainEventsToo += EventHandler;
        }

        void EventHandler(object sender, EventArgs e)
        {

        }
    }
}
