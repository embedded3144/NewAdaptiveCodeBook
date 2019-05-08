using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter03._3_26
{
    public interface ITargetInterface
    {
        void DoSomething();
    }

    public static class MixinExtensions
    {
        public static void FirstExtensionMethod(this ITargetInterface target)
        {
            Console.WriteLine("첫번째 확장 메서드가 호출되었음.");
        }

        public static void SecondExtensionMethod(this ITargetInterface target)
        {
            Console.WriteLine("두번째 확장 메서드가 호출되었음.");
        }
    }

    public static class MoreMixinExtensions
    {
        public static void FurtherExtensionMethodA(this ITargetInterface target, int extraParameter)
        {
            Console.WriteLine("매개변수 {0}를 이용해 추가 확장 메서드 A가 호출됨", extraParameter);
        }

        public static void FurtherExtensionMethodB(this ITargetInterface target, string stringPrameter)
        {
            Console.WriteLine("매개변수 {0}를 이용해 추가 확장 메서드 B가 호출됨", stringPrameter);
        }
    }
    
    public class MixinClient
    {
        public MixinClient(ITargetInterface target)
        {
            this.target = target;
        }

        public void Run()
        {
            target.DoSomething();
            target.FirstExtensionMethod();
            target.SecondExtensionMethod();
            target.FurtherExtensionMethodA(30);
            target.FurtherExtensionMethodB("안녕하세요!");
        }

        private readonly ITargetInterface target;
    }
}
