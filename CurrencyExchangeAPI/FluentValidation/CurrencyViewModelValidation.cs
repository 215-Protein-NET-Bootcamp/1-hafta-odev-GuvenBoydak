using CurrencyExchangeAPI.Models;
using FluentValidation;

namespace CurrencyExchangeAPI.FluentValidation
{
    public class CurrencyViewModelValidation : AbstractValidator<CurrencyViewModel>
    {
        public CurrencyViewModelValidation()
        {
            RuleFor(x => x.From).NotEmpty().Matches("^[a-zA-Z]*$").Length(3);
            RuleFor(x => x.To).NotEmpty().Matches("^[a-zA-Z]*$").Length(3);
            RuleFor(x => x.Amount).NotEmpty().GreaterThan(1);
        }
    }
}
