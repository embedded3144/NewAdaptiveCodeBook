using System;
using System.Collections.Generic;
using System.Text;

namespace Chapter04
{
    public interface IAccountRepository
    {
        IAccount GetByName(string uniueAccountName);
        void NewAccount(AccountBase newAccount);
    }

    public class FakeAccountRepository : IAccountRepository
    {
        private IAccount account;

        public FakeAccountRepository(IAccount account)
        {
            this.account = account;
        }

        public IAccount GetByName(string accountName)
        {
            return account;
        }

        //public Account IAccountRepository.GetByName(string uniueAccountName)
        //{
        //    return account;
        //}

        public void NewAccount(AccountBase newAccount)
        {
            throw new NotImplementedException();
        }
    }
}
