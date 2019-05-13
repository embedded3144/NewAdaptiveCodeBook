using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter05._3_추상화를위한리팩토링
{
    public interface ITradeDataProvider
    {
        IEnumerable<string> GetTradeData();
    }
}
