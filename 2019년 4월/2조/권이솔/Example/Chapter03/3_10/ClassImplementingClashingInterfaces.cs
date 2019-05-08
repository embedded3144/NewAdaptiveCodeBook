using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter03._3_10
{
    public class ClassImplementingClashingInterfaces : IInterfaceOne, IAnotherInterfaceOne
    {
        void IInterfaceOne.MethodOne()
        {
        }

        void IAnotherInterfaceOne.MethodOne()
        {
        }
    }
}
