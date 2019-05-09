using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceExample
{
    public class ClashingMethodSignatures
    {
        public void MethodA()
        {

        }

        // 이 메서드는 위에 정의한 매서드의 시그너처와 충돌한다.
        //public void MethodA()
        //{

        //}

        // 이 매서드 역시 리턴 값은 시그너처에 포함되지 않기 때문에 충돌이 발생한다.
        //public int MethodA()
        //{
        //    return 0;
        //}

        public int MethodB(int x)
        {
            return x;
        }

        // 이번에는 매개변수가 다르기 때문에 충돌이 발생하지 않는다.
        // 이는 위에 정의한 MethodB를 오버로드한 형태이다.
        public int MethodB(int x, int y)
        {
            return x + y;
        }
    }
}
