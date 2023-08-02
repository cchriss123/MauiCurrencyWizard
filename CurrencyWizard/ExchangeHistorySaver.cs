using System;
using System.Globalization;

namespace CurrencyWizard
{
	public class ExchangeHistorySaver
	{
        public static void SaveExchanges(CurrencyExchange exchange, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))   // true to append to file rather than overwrite
            {
                writer.WriteLine($"" +
                      $"{exchange.FromCode}," +
                      $"{exchange.ToCode}," +
                      $"{exchange.Price}," +           
                      $"{exchange.AmountPaid}," +
                      $"{exchange.AmountReceived}," +
                      $"{exchange.Date}");
            }
        }
    }
}

