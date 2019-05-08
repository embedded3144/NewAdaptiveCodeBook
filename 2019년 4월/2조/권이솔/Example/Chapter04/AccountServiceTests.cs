using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chapter04
{
    [TestClass]
    public class AccountServiceTests
    {
        [TestMethod]
        public void AddingTransactionToAccountDelegatesToAccountInstance()
        {
            // arrange
            var account = new Account(new BronzeRewardCard());
            var mockRepository = new Mock<IAccountRepository>();
            mockRepository.Setup(r => r.GetByName("예금 계좌")).Returns(account);
            //var fakeRepository = new FakeAccountRepository(account);
            var sut = new AccountService(mockFactory.Object, mockRepository.Object);

            // action
            sut.AddTransactionToAccount("예금 계좌", 200m);

            // assert
            Assert.AreEqual(200m, account.Balance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotCreteAccountServiceWithNullAccountRepository()
        {
            // arrange

            // action
            new AccountService(null, null);

            // assert
        }

        [TestMethod]
        public void DoNotThrowWhenAccountIsNotFound()
        {
            // arrange
            var mockRepository = new Mock<IAccountRepository>();
            var sut = new AccountService(mockFactory.Object, mockRepository.Object);

            // action
            sut.AddTransactionToAccount("예금 계좌", 100m);

            // assert
        }

        [TestMethod]
        public void AccountExceptionsAreWrappedInThrowServiceException()
        {
            // arrange
            var account = new Mock<IAccount>();
            account.Setup(a => a.AddTransaction(100m)).Throws<DomainException>();
            var mockRepository = new Mock<IAccountRepository>();
            mockRepository.Setup(r => r.GetByName("예금 계좌")).Returns(account.Object);
            var sut = new AccountService(mockFactory.Object, mockRepository.Object);

            // action
            try
            {
                sut.AddTransactionToAccount("예금 계좌", 100m);
            }
            catch(ServiceException serviceException)
            {
                // assert
                Assert.IsInstanceOfType(serviceException.InnerException, typeof(DomainException));
            }
        }

        private Mock<Account> mockAccount;
        private Mock<IAccountRepository> mockRepository;
        private Mock<IAccountFactory> mockFactory;
        private AccountService sut;

        [TestInitialize]
        public void Setup()
        {
            mockAccount = new Mock<Account>();
            mockRepository = new Mock<IAccountRepository>();
            mockFactory = new Mock<IAccountFactory>();
            sut = new AccountService(mockFactory.Object, mockRepository.Object);
        }

        [TestMethod]
        public void AccountExceptionsAreWrappedInThrowServiceException_2()
        {
            // arrange
            mockAccount.Setup(a => a.AddTransaction(100m)).Throws<DomainException>();
            mockRepository.Setup(r => r.GetByName("예금 계좌")).Returns(mockAccount.Object);

            // action
            try
            {
                sut.AddTransactionToAccount("예금 계좌", 100m);
            }
            catch (ServiceException serviceException)
            {
                // assert
                Assert.IsInstanceOfType(serviceException.InnerException, typeof(DomainException));
            }
        }
    }
}
