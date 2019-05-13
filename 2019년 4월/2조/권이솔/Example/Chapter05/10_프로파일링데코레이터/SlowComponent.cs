using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter05._10_프로파일링데코레이터
{
    class SlowComponent : IComponent
    {
        private readonly Random random;
        private readonly Stopwatch stopwatch;

        public SlowComponent()
        {
            random = new Random((int)DateTime.Now.Ticks);
            stopwatch = new Stopwatch();
        }

        public void Something()
        {
            stopwatch.Start();
            for(var i=0;i<100;++i)
            {
                Thread.Sleep(random.Next(i) * 10);
            }
            stopwatch.Stop();
            Console.WriteLine("메서트 실행 시간 : {0}", stopwatch.ElapsedMilliseconds / 1000);
        }
    }
}
