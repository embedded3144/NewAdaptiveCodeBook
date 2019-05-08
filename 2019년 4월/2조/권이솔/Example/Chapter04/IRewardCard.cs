using System;
using System.Collections.Generic;
using System.Text;

namespace Chapter04
{
    public interface IRewardCard
    {
        int RewardPoints
        {
            get;
        }

        void CalculateRewardPoints(decimal transactionAmount, decimal accountBalance);
    }
}
