using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter03._3_9
{
    public class ClassAvoidingMethodSignatureClash : IInterfaceOne
    {
        public void MethodOne()
        {
            // 원래의 구현 코드
        }

        void IInterfaceOne.MethodOne()
        {
            // 새로운 구현 코드
        }
    }
}
