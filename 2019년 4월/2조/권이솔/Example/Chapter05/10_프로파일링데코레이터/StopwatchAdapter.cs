using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter05._10_프로파일링데코레이터
{
    class StopwatchAdapter : IStopWatch
    {
        private readonly Stopwatch stopwatch;
        public StopwatchAdapter(Stopwatch stopwatch)
        {
            this.stopwatch = stopwatch;
        }

        public void Start()
        {
            stopwatch.Start();
        }

        public long Stop()
        {
            stopwatch.Stop();
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
            return elapsedMilliseconds;
        }
    }
}
