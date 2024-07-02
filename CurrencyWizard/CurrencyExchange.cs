
namespace CurrencyWizard
{
    public class CurrencyExchange
    {
        public string FromCode { get; set; }
        public string ToCode { get; set; }
        public string Price { get; set; }
        public string AmountPaid { get; set; }
        public string AmountReceived { get; set; }
        public string Date { get; set; }


        public CurrencyExchange(string fromCode, string toCode, string price, string amountPaid, string amountReceived, string date)
        {
            FromCode = fromCode;
            ToCode = toCode;
            Price = price.Replace(',', '.');
            AmountPaid = amountPaid.Replace(',', '.');
            AmountReceived = amountReceived.Replace(',', '.');
            Date = date;
        }


        private string GetSpacing(string str, int space)
        {
            return str.PadRight(space - str.Length , ' ');
        }


        public override string ToString()
        {
            return $"Date: {GetSpacing(Date, 45)} " +
                $"Price: {GetSpacing(Price, 35)} " +
                $"From: {FromCode} {GetSpacing(AmountPaid, 35)} " +
                $"To: {ToCode} {AmountReceived}";
        }

    }
}

