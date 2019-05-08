using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter03._3_11
{
    interface IUser
    {
        void IncrementsSessionTicket();
        bool IsNull { get; }
    }
}
