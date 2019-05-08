using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chapter04
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void AddingTransactionChangesBalance()
        {
            // arrange
            var account = new Account();

            // action
            account.AddTransaction(200m);

            // assert
            Assert.AreEqual(200m, account.Balance);
        }

        [TestMethod]
        public void AccountHaveAnOpeningBalanceOfZero()
        {
            // arrange

            // action
            var account = new Account();

            // assert
            Assert.AreEqual(0m, account.Balance);
        }

        [TestMethod]
        public void Adding100TranactionChagesBalance()
        {
            // arrange
            var account = new Account();

            // action
            account.AddTransaction(100m);

            // assert
            Assert.AreEqual(100m, account.Balance);
        }

        [TestMethod]
        public void AddingTransactionCreatesSummationBalance()
        {
            // arrange
            var account = new Account();
            account.AddTransaction(50m);

            // action
            account.AddTransaction(75m);

            // assert
            Assert.AreEqual(125m, account.Balance);
        }
    }
}
