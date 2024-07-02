namespace CurrencyWizard;

public partial class MainPage : ContentPage
{
    private string fromCode;
    private string toCode;
    private double exchangeRate;
    private double price;
    private double amountInFromCurrency;
    private double amountInToCurrency;


    string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "exchange_history.csv");

    List<CurrencyExchange> currencyExchanges = new List<CurrencyExchange>();

    List<string> currencyCodes = new List<string>()     
            {
                "EUR", "USD", "JPY", "BGN", "CZK", "DKK", "GBP", "HUF", "PLN",
                "RON", "SEK", "CHF", "ISK", "NOK", "TRY", "AUD", "BRL", "CAD",
                "CNY", "HKD", "IDR", "ILS", "INR", "KRW", "MXN", "MYR", "NZD",
                "PHP", "SGD", "THB", "ZAR"
            };

    public MainPage()
    {
        InitializeComponent();
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();

        comboBoxFrom.ItemsSource = currencyCodes;
        comboBoxTo.ItemsSource = currencyCodes;
        comboBoxFrom.SelectedItem = "SEK";
        comboBoxTo.SelectedItem = "USD";

        ExchangeHistoryLoader.LoadExchanges(currencyExchanges, filePath);

        var reversedLines = currencyExchanges.Select(exchange => exchange.ToString()).Reverse();
        listViewHistory.ItemsSource = reversedLines;

    }



    private async void Calculate_Click(object sender, EventArgs e)
    {
        if (!double.TryParse(textBoxAmount.Text, out amountInFromCurrency))

        {
            // use a dialog to show the error
            await DisplayAlert("Error", "Please enter a valid number for the amount.", "OK");
            return;
        }

        fromCode = comboBoxFrom.SelectedItem as string;
        toCode = comboBoxTo.SelectedItem as string;

        exchangeRate = await ExchangeRateProvider.GetExchangeRate(fromCode, toCode, this);
        price = Math.Round(1 / exchangeRate, 2);
        amountInToCurrency = Math.Round(exchangeRate * amountInFromCurrency, 2);
       
        textBoxResult.Text = $"From: {fromCode} | To: {toCode} | price: {price} | amount paid: {amountInFromCurrency} | amount recived: {amountInToCurrency}";
    }


    private async void Confirm_Click(object sender, EventArgs e)
    {
        if (exchangeRate == 0)
        {
            await DisplayAlert("Error", "Please calculate the exchange rate first.", "OK");
            return;
        }

        CurrencyExchange currencyExchange = new CurrencyExchange(
            fromCode,
            toCode,
            price.ToString(),
            amountInFromCurrency.ToString(),
            amountInToCurrency.ToString(),
            DateTime.Now.ToString("yyyy-MM-dd"));

        currencyExchanges.Add(currencyExchange);

        ExchangeHistorySaver.SaveExchanges(currencyExchange, filePath);

        var reversedLines = currencyExchanges.Select(exchange => exchange.ToString()).Reverse();

        listViewHistory.ItemsSource = reversedLines.ToList();
    }



}


