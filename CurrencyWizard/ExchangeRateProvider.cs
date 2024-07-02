using Newtonsoft.Json.Linq;


namespace CurrencyWizard
{
    public class ExchangeRateProvider
    {
        public static async Task<double> GetExchangeRate(string fromCode, string toCode, Page page)
        {
            string apiKey = "nk***********************************************";
            string urlString = "https://anyapi.io/api/v1/exchange/rates?base=" + fromCode.ToUpper() + "&apiKey=" + apiKey;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(urlString);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(result);
                double rate = (double)json["rates"][toCode];
                return rate;
            }
            await page.DisplayAlert("Error", "Unable to fetch data from the API. Response code: " + response.StatusCode, "OK");
            return 0;
        }
    }

}

