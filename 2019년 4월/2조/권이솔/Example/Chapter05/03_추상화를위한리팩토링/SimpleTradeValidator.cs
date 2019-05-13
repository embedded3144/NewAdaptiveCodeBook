using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter05._3_추상화를위한리팩토링
{
    public class SimpleTradeValidator : ITradeValidator
    {
        private readonly ILog logger;
        public SimpleTradeValidator(ILog logger)
        {
            this.logger = logger;
        }

        public bool Validate(string[] tradeData)
        {
            if (tradeData.Length != 3)
            {
                logger.WarnFormat("경고: {0}번째 줄에서 데이터 오류를 발견했습니다. {1}개의 필드만 발견되었습니다.", tradeData.Length);
                return false;
            }

            if (tradeData[0].Length != 6)
            {
                logger.WarnFormat("경고: {0}번째 줄의 환율에서 오류를 발견했습니다:'{1}", tradeData[0]);
                return false;
            }

            int tradeAmount;
            if (!int.TryParse(tradeData[1], out tradeAmount))
            {
                logger.WarnFormat("경고: {0}번째 줄의 거래 물품 수가 올바르지 않습니다:'{1}'", tradeData[1]);
                return false;
            }

            decimal tradePrice;
            if (!decimal.TryParse(tradeData[2], out tradePrice))
            {
                logger.WarnFormat("경고: {0}번째 줄의 거래 금액이 올바르지 않습니다:'{1}'", tradeData[2]);
                return false;
            }

            return true;
        }
    }
}
