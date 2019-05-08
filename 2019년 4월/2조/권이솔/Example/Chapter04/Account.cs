using System;

namespace Chapter04
{
    public interface IAccount
    {
        void AddTransaction(decimal amount);
    }
    public class Account : IAccount
    {
        private readonly AccountType type;
        private readonly IRewardCard rewardCard;
        public decimal Balance { get; private set; }

        public Account() : this(new BronzeRewardCard()) { }
        public Account(IRewardCard rewardCard)
        {
            this.rewardCard = rewardCard;
        }

        public void AddTransaction(decimal amount)
        {
            rewardCard.CalculateRewardPoints(amount, Balance);
            Balance += amount;
        }
    }

    public abstract class AccountBase
    {
        public static AccountBase CreateAccount(AccountType type)
        {
            AccountBase account = null;
            switch(type)
            {
                case AccountType.Bronze:
                    account = new BronzeAccount();
                    break;
                case AccountType.Silver:
                    account = new SilverAccount();
                    break;
                case AccountType.Gold:
                    account = new GoldAccount();
                    break;
                case AccountType.Platinum:
                    account = new PlatinumAccount();
                    break;
            }
            return account;
        }

        //private readonly AccountType type;
        public decimal Balance { get; private set; }
        public int RewardPoints { get; private set; }

        public void AddTransaction(decimal amount)
        {
            RewardPoints += CalculateRewardPoints(amount);
            Balance += amount;
        }

        public abstract int CalculateRewardPoints(decimal amount);
    }

    public class SilverAccount : AccountBase
    {
        private const int SilverTransactionCostPerPoint = 10;

        public override int CalculateRewardPoints(decimal amount)
        {
            return Math.Max((int)decimal.Floor(amount / SilverTransactionCostPerPoint), 0);
        }
    }

    public class GoldAccount : AccountBase
    {
        private const int GoldBalanceCostPerPoint = 2000;
        private const int GoldTransactionCostPerPoint = 5;

        public override int CalculateRewardPoints(decimal amount)
        {
            return Math.Max((int)decimal.Floor((Balance / GoldBalanceCostPerPoint) + (amount / GoldTransactionCostPerPoint)), 0);
        }
    }

    public class PlatinumAccount : AccountBase
    {
        private const int PlatinumTransactionCostPerPoint = 2;
        private const int PlatinumBalanceCostPerPoint = 1000;

        public override int CalculateRewardPoints(decimal amount)
        {
            return Math.Max((int)decimal.Ceiling((Balance / 10000 * PlatinumBalanceCostPerPoint) + (amount / PlatinumTransactionCostPerPoint)), 0);
        }
    }

    public class BronzeAccount : AccountBase
    {
        private const int BronzeTransactionCostPerPoint = 2;

        public override int CalculateRewardPoints(decimal amount)
        {
            return Math.Max((int)decimal.Floor(amount / BronzeTransactionCostPerPoint), 0);
        }
    }

    public class StandardAccount : AccountBase
    {
        public override int CalculateRewardPoints(decimal amount)
        {
            return 0;
        }
    }


}