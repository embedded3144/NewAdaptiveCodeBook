using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter05._1_문제의정의
{
    public class TradeProcessor
    {
        public void ProcessTrades(Stream stream)
        {
            // 행을 읽는다
            var lines = new List<string>();
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            var trades = new List<TradeRecord>();

            var lineCount = 1;
            foreach (var line in lines)
            {
                var fields = line.Split(new char[] { ',' });
                if (fields.Length != 3)
                {
                    Console.WriteLine("경고: {0}번째 줄에서 데이터 오류를 발견했습니다. {1}개의 필드만 발견되었습니다.", lineCount, fields.Length);
                }

                if (fields[0].Length != 6)
                {
                    Console.WriteLine("경고: {0}번째 줄의 환율에서 오류를 발견했습니다:'{1}", lineCount, fields[0]);
                    continue;
                }

                int tradeAmount;
                if (!int.TryParse(fields[1], out tradeAmount))
                {
                    Console.WriteLine("경고: {0}번째 줄의 거래 물품 수가 올바르지 않습니다:'{1}'", lineCount, fields[1]);
                }

                decimal tradePrice;
                if (!decimal.TryParse(fields[2], out tradePrice))
                {
                    Console.WriteLine("경고: {0}번째 줄의 거래 금액이 올바르지 않습니다:'{1}'", lineCount, fields[2]);
                }

                var sourceCurrencyCode = fields[0].Substring(0, 3);
                var destinationCurrencyCode = fields[0].Substring(3, 3);

                // 값을 계산한다.
                var trade = new TradeRecord
                {
                    SourceCurrency = sourceCurrencyCode,
                    DestinationCurrency = destinationCurrencyCode,
                    Lots = tradeAmount / LotSize,
                    Price = tradePrice
                };

                trades.Add(trade);
                lineCount++;
            }

            using (var connection = new SqlConnection("DataSource=(local);Initial Catalog=TradeDatabase;Intergrated Security=True"))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var trade in trades)
                    {
                        var command = connection.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "dbo.insert_trade";
                        command.Parameters.AddWithValue("@sourceCurrency", trade.SourceCurrency);
                        command.Parameters.AddWithValue("@destinationCurrency", trade.DestinationCurrency);
                        command.Parameters.AddWithValue("@lots", trade.Lots);
                        command.Parameters.AddWithValue("@price", trade.Price);
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                connection.Close();
            }

            Console.WriteLine("정보: {0}건의 거래 내역이 처리되었습니다.", trades.Count);
        }

        private static float LotSize = 100000f;
    }
}
