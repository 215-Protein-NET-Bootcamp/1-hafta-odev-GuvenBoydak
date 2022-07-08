using CurrencyExchangeAPI.FluentValidation;
using CurrencyExchangeAPI.Models;
using CurrencyExchangeAPI.Utilities.Results;
using FluentValidation;
using RestSharp;
using System.Text.Json;

namespace CurrencyExchangeAPI.Services
{
    public class CurrencyManager : ICurrencyService
    {
        private readonly IConfiguration _config;

        public CurrencyManager(IConfiguration config)
        {
            _config = config;
        }

        public IDataResult<Currency> ConvertCurrency(CurrencyViewModel model)
        {
            CurrencyViewModelValidation validator = new CurrencyViewModelValidation();
            validator.ValidateAndThrow(model);

            string url = _config.GetSection("BaseUrl").Value;

            RestClient client = new RestClient($"{url}currency_data/convert?to={model.To}&from={model.From}&amount={model.Amount}");

            RestRequest request = new RestRequest("", Method.Get);
           
            request.AddHeader("apikey", _config.GetSection("ApiKey").Value);

            RestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                JsonDocument parse = JsonDocument.Parse(response.Content);
                double result = parse.RootElement.GetProperty("result").Deserialize<double>();

                return new SuccessDataResult<Currency>(new Currency() { Amount = model.Amount, From = model.From, To = model.To,Result=result},"Kur Hesaplama Başarılı");
            }

            return new ErrorDataResult<Currency>("Başarısız işlem");
           
        }

        public string GetAllCurrency()
        {
            string url = _config.GetSection("BaseUrl").Value;

            RestClient client = new RestClient($"{url}currency_data/list");

            RestRequest request = new RestRequest("", Method.Get);
            request.AddHeader("apikey", _config.GetSection("ApiKey").Value);

           RestResponse response = client.Execute(request);

            return response.Content;
        }

        public IDataResult<Excahange> GetSeletedCurrency(ExcahangeViewModel model)
        {
            ExchangeViewModelValidator validator = new ExchangeViewModelValidator();
            validator.ValidateAndThrow(model);

            string url = _config.GetSection("BaseUrl").Value;
            RestClient client = new RestClient($"{url}exchangerates_data/latest?symbols={model.Symbol}&base={model.Base}");

            RestRequest request = new RestRequest("", Method.Get);
            request.AddHeader("apikey", _config.GetSection("ApiKey").Value);

            RestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                JsonDocument parse = JsonDocument.Parse(response.Content);
                string baseCurrency = parse.RootElement.GetProperty("base").Deserialize<string>();
                IDictionary<string, double> ratesCurrency = parse.RootElement.GetProperty("rates").Deserialize<IDictionary<string,double>>();

                return new SuccessDataResult<Excahange>(new Excahange() {Base= baseCurrency, Rates=ratesCurrency },"Listeleme Başarılı");
            }
            return new ErrorDataResult<Excahange>("Başarısız işlem");
        }
    }
}
