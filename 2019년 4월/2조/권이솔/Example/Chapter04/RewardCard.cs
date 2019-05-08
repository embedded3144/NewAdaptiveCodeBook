using System;
using System.Collections.Generic;
using System.Text;

namespace Chapter04
{
    internal class BronzeRewardCard : IRewardCard
    {
        public int RewardPoints { get; private set; }

        public void CalculateRewardPoints(decimal transactionAmount, decimal accountBalance)
        {
            RewardPoints += Math.Max((int)decimal.Floor(transactionAmount / BronzeTransactionCostPerPoint), 0);
        }

        private const int BronzeTransactionCostPerPoint = 20;
    }

    internal class PlatinumRewardCard : IRewardCard
    {
        public int RewardPoints { get; private set; }

        public void CalculateRewardPoints(decimal transactionAmount, decimal accountBalance)
        {
            RewardPoints += Math.Max((int)decimal.Ceiling((accountBalance / 10000 * PlatinumBalanceCostPerPoint) + (transactionAmount / PlatinumTransactionCostPerPoint)), 0);
        }

        private const int PlatinumTransactionCostPerPoint = 2;
        private const int PlatinumBalanceCostPerPoint = 1000;
    }

    internal class SilverAccountCard : IRewardCard
    {
        private const int SilverTransactionCostPerPoint = 10;

        public int RewardPoints { get; private set; }

        public void CalculateRewardPoints(decimal transactionAmount, decimal accountBalance)
        {
            RewardPoints += Math.Max((int)decimal.Floor(accountBalance / SilverTransactionCostPerPoint), 0);
        }
    }

    internal class GoldAccountCard : IRewardCard
    {
        private const int GoldBalanceCostPerPoint = 2000;
        private const int GoldTransactionCostPerPoint = 5;

        public int RewardPoints { get; private set; }

        public void CalculateRewardPoints(decimal transactionAmount, decimal accountBalance)
        {
            RewardPoints += Math.Max((int)decimal.Floor((accountBalance / GoldBalanceCostPerPoint) + (transactionAmount / GoldTransactionCostPerPoint)), 0);
        }
    }
}
