using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter05._6_조건부데코레이터
{
    public class DateTester
    {
        public bool TodayIsAnEvenDatyOfTheMonth
        {
            get
            {
                return DateTime.Now.Day % 2 == 0;
            }
        }
    }

    
}
