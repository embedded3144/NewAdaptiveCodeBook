using System;

namespace Chapter05._12_속성과이벤트데코레이트하기
{
    public interface IComponant
    {
        string Property { get; set; }
        event EventHandler Event;
    }
}