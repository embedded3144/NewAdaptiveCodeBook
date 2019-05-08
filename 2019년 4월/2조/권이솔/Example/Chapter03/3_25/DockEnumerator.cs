using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter03._3_25
{
    class DockEnumerator
    {
        int i = 0;

        public bool MoveNext()
        {
            return i++ < 10;
        }

        public int Current
        {
            get
            {
                return i;
            }
        }
    }
}
