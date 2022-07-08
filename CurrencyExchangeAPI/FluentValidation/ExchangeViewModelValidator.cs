using CurrencyExchangeAPI.Models;
using FluentValidation;

namespace CurrencyExchangeAPI.FluentValidation
{
    public class ExchangeViewModelValidator:AbstractValidator<ExcahangeViewModel>
    {
        public ExchangeViewModelValidator()
        {
            RuleFor(x=>x.Base).NotEmpty().Matches("^[a-zA-Z]*$").Length(3);
            RuleFor(x=>x.Symbol).NotEmpty().Matches("^[a-zA-Z]+(,[a-zA-Z]+)*$");
        }
    }
}
