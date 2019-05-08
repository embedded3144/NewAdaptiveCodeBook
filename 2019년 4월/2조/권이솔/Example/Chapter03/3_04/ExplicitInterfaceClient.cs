using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter03
{
    public class ExplicitInterfaceClient
    {
        public ExplicitInterfaceClient(ExplicitInterfaceImplementation implementationReference
            , ISimpleInterface interfaceReference)
        {
            //var instancePropertyValue =
            //    implementationReference.ThisIntegerPropertyOnlyNeedsAGetter;
            //implementationReference.ThisMethodRequiresImplementation();
            //implementationReference.ThisStringPropertyNeedsImplementingToo = "안녕하세요";
            //implementationReference.InterfaceCanContainEventsToo += EventHandler;

            var interfacePropertyValue = interfaceReference.ThisIntegerPropertyOnlyNeedsAGetter;
            interfaceReference.ThisMethodRequiresImplementation();
            interfaceReference.ThisStringPropertyNeedsImplementingToo = "안녕하세요";
            interfaceReference.InterfaceCanContainEventsToo += EventHandler;
        }

        void EventHandler(object sender, EventArgs e)
        {

        }
    }
}
