using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter05._3_추상화를위한리팩토링
{
    public class TradeProcessor
    {
        public TradeProcessor(ITradeDataProvider tradeDataProvider, ITradeParser tradeParser, ITradeStorage tradeStorage)
        {
            this.tradeDataProvider = tradeDataProvider;
            this.tradeParser = tradeParser;
            this.tradeStorage = tradeStorage;
        }

        public void ProcessTrades()
        {
            var lines = tradeDataProvider.GetTradeData();
            var trades = tradeParser.Parse(lines);
            tradeStorage.Persist(trades);
        }

        private readonly ITradeDataProvider tradeDataProvider;
        private readonly ITradeParser tradeParser;
        private readonly ITradeStorage tradeStorage;





        public void ProcessTrades(Stream stream)
        {
            var lines = ReadTradeData(stream);
            var trades = ParseTrades(lines);
            StoreTrades(trades);
        }

        private void StoreTrades(IEnumerable<TradeRecord> trades)
        {
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

            LogMessage("정보: {0}건의 거래 내역이 처리되었습니다.", trades.Count());
        }

        private IEnumerable<TradeRecord> ParseTrades(IEnumerable<string> tradData)
        {
            var trades = new List<TradeRecord>();
            var lineCount = 1;
            foreach(var line in tradData)
            {
                var fields = line.Split(new char[] { ',' });
                if(!ValidateTradeData(fields, lineCount))
                {
                    continue;
                }
                var trade = MapTradeDataToTradeRecord(fields);
                trades.Add(trade);
                lineCount++;
            }
            return trades;
        }

        private TradeRecord MapTradeDataToTradeRecord(string[] fields)
        {
            var sourceCurrencyCode = fields[0].Substring(0, 3);
            var destinationCurrencyCode = fields[0].Substring(3, 3);
            var tradeAmount = int.Parse(fields[1]);
            var tradePrice = decimal.Parse(fields[2]);

            var tradeRecord = new TradeRecord
            {
                SourceCurrency = sourceCurrencyCode,
                DestinationCurrency = destinationCurrencyCode,
                Lots = tradeAmount / LotSize,
                Price = tradePrice
            };
            return tradeRecord;
        }

        private bool ValidateTradeData(string[] fields, int lineCount)
        {
            if (fields.Length != 3)
            {
                LogMessage("경고: {0}번째 줄에서 데이터 오류를 발견했습니다. {1}개의 필드만 발견되었습니다.", lineCount, fields.Length);
                return false;
            }

            if (fields[0].Length != 6)
            {
                LogMessage("경고: {0}번째 줄의 환율에서 오류를 발견했습니다:'{1}", lineCount, fields[0]);
                return false;
            }

            int tradeAmount;
            if (!int.TryParse(fields[1], out tradeAmount))
            {
                LogMessage("경고: {0}번째 줄의 거래 물품 수가 올바르지 않습니다:'{1}'", lineCount, fields[1]);
                return false;
            }

            decimal tradePrice;
            if (!decimal.TryParse(fields[2], out tradePrice))
            {
                LogMessage("경고: {0}번째 줄의 거래 금액이 올바르지 않습니다:'{1}'", lineCount, fields[2]);
                return false;
            }

            return true;
        }

        private void LogMessage(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }

        private IEnumerable<string> ReadTradeData(Stream stream)
        {
            var tradeData = new List<string>();
            using (var reader = new StreamReader(stream))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    tradeData.Add(line);
                }
            }
            return tradeData;
        }

        public void ProcessTrades_Old(Stream stream)
        {
            // 행을 읽는다
            var lines = new List<string>();
            using (var reader = new StreamReader(stream))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            var trades = new List<TradeRecord>();

            var lineCount = 1;
            foreach(var line in lines)
            {
                var fields = line.Split(new char[] { ',' });
                if(fields.Length != 3)
                {
                    Console.WriteLine("경고: {0}번째 줄에서 데이터 오류를 발견했습니다. {1}개의 필드만 발견되었습니다.", lineCount, fields.Length);
                }

                if(fields[0].Length != 6)
                {
                    Console.WriteLine("경고: {0}번째 줄의 환율에서 오류를 발견했습니다:'{1}", lineCount, fields[0]);
                    continue;
                }

                int tradeAmount;
                if(!int.TryParse(fields[1], out tradeAmount))
                {
                    Console.WriteLine("경고: {0}번째 줄의 거래 물품 수가 올바르지 않습니다:'{1}'", lineCount, fields[1]);
                }

                decimal tradePrice;
                if(!decimal.TryParse(fields[2], out tradePrice))
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
                    foreach(var trade in trades)
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
