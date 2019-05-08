using System;

namespace Chapter04
{
    public interface IAccountService
    {

    }


    public class AccountService : IAccountService
    {
        private IAccountRepository accountRepository;
        private IAccountFactory accountFactory;
        public AccountService(IAccountFactory accountFactory, IAccountRepository repository)
        {
            if (repository == null || accountFactory == null)
                throw new ArgumentNullException("repository", "유효한 저장소 객체를 제공해야 합니다.");
            this.accountFactory = accountFactory;
            this.accountRepository = repository;

        }

        public void CrateAccount(AccountType accountType)
        {
            var newAccount = accountFactory.CreateAccount(accountType);
            accountRepository.NewAccount(newAccount);
        }

        public AccountBase CrateAccount(string accountType)
        {
            var objectHandle = Activator.CreateInstance(null, string.Format("{0}Account", accountType));
            return (AccountBase)objectHandle;
        }

        public void AddTransactionToAccount(string uniueAccountName, decimal transactionAmount)
        {
            var account = accountRepository.GetByName(uniueAccountName);
            if(account != null)
            {
                try
                {
                    account.AddTransaction(transactionAmount);
                }
                catch(DomainException domainException)
                {
                    throw new ServiceException("도메인 모델에서 예외가 발생했습니다.", domainException);
                }
            }
        }
    }
}