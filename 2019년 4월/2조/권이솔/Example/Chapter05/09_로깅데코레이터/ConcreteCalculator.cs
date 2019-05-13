using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter05._9_로깅데코레이터
{
    class ConcreteCalculator : ICalculator
    {
        public int Add(int x, int y)
        {
            return x + y;
        }
    }
}
