using System;
using System.Collections.Generic;
using System.Text;

namespace Chapter04
{
    public interface IAccountFactory
    {
        AccountBase CreateAccount(AccountType accountType);
    }
}
