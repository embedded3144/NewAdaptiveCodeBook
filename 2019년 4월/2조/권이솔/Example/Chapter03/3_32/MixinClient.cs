using Chapter03._3_29;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter03._3_32
{
    public class MixinClient
    {
        public MixinClient(ITargetInterface target)
        {
            this.target = target;
        }

        public void Run()
        {
            target.DoSomething();

            var targetAsMixinA = target as IMixinInterfaceA;
            if(targetAsMixinA != null)
            {
                targetAsMixinA.MethodA();
            }

            var targetAsMixinB = target as IMixinInterfaceB;
            if(targetAsMixinB != null)
            {
                targetAsMixinB.MethodB(30);
            }

            var targetAsMixinC = target as IMixinInterfaceC;
            if(targetAsMixinC != null)
            {
                targetAsMixinC.MethodC("안녕하세요!");
            }
        }

        public readonly ITargetInterface target;
    }
}
