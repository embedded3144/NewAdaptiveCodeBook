using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter05._10_프로파일링데코레이터
{
    public class LoggingStopwatch : IStopWatch
    {
        private readonly IStopWatch decoratedStopwatch;

        public LoggingStopwatch(IStopWatch decoratedStopwatch)
        {
            this.decoratedStopwatch = decoratedStopwatch;
        }

        public void Start()
        {
            decoratedStopwatch.Start();
            Console.WriteLine("스탑워치가 시작되었습니다.");
        }

        public long Stop()
        {
            var elapsedMilliseconds = decoratedStopwatch.Stop();
            Console.WriteLine("스톱워치가 {0}초 후에 정지되었습니다.", TimeSpan.FromMilliseconds(elapsedMilliseconds).TotalSeconds);
            return elapsedMilliseconds;
        }
    }
}
