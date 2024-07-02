using System.Globalization;

namespace CurrencyWizard
{
    public class ExchangeHistoryLoader
    {
        public static void LoadExchanges(List<CurrencyExchange> currencyExchanges, string filePath)
        {
            if (!File.Exists(filePath))
                return;


            currencyExchanges.AddRange(
                File.ReadAllLines(filePath)
                    .Select(line => line.Split(','))        
                    .Where(values => values.Length == 6)    
                    .Select(values => new CurrencyExchange(
                        values[0],
                        values[1],
                        values[2],
                        values[3],
                        values[4],
                        values[5])));

        }
    }
}


