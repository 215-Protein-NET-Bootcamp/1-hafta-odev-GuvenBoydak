using CurrencyExchangeAPI.Utilities.Results;


namespace CurrencyExchangeAPI.Utilities.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message) : base(false, message)
        {
        }

        public ErrorResult() : base(false)
        {

        }
    }
}