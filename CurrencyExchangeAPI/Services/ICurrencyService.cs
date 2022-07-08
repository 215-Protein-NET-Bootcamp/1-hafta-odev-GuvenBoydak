using CurrencyExchangeAPI.Models;
using CurrencyExchangeAPI.Utilities.Results;
using RestSharp;

namespace CurrencyExchangeAPI.Services
{
    public interface ICurrencyService
    {

        IDataResult<Currency> ConvertCurrency(CurrencyViewModel model);

        string GetAllCurrency();

        IDataResult<Excahange> GetSeletedCurrency(ExcahangeViewModel model);
    }
}
