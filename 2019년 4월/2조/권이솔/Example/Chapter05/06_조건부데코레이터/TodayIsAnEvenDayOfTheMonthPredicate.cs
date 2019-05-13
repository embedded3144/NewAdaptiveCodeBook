using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter05._6_조건부데코레이터
{
    class TodayIsAnEvenDayOfTheMonthPredicate
    {
        private readonly DateTester dateTester;
        public TodayIsAnEvenDayOfTheMonthPredicate(DateTester dateTester)
        {
            this.dateTester = dateTester;
        }

        public bool Test()
        {
            return dateTester.TodayIsAnEvenDatyOfTheMonth;
        }
    }
}
