using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter05._10_프로파일링데코레이터
{
    public class ProfilingComponent : IComponent
    {
        private readonly IComponent decoratedComponent;
        private readonly IStopWatch stopwatch;

        public ProfilingComponent(IComponent decoratedComponent, IStopWatch stopWatch)
        {
            this.decoratedComponent = decoratedComponent;
            this.stopwatch = stopWatch;
        }

        public void Something()
        {
            stopwatch.Start();
            decoratedComponent.Something();
            var elapsedMilliseconds = stopwatch.Stop();
            Console.WriteLine("메서드 실행 시간: {0}", elapsedMilliseconds / 1000);
        }
    }
}
