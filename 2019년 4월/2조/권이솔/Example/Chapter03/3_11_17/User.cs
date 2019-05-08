using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter03._3_11
{
    public class User : IUser
    {
        public bool IsNull
        {
            get
            {
                return false;
            }
        }

        public string Name { get; set; }
        public Guid Id { get; internal set; }

        private DateTime sessionExpiry;

        public void IncrementsSessionTicket()
        {
            throw new NotImplementedException();
        }

        public User(Guid grid)
        {

        }
    }
}
