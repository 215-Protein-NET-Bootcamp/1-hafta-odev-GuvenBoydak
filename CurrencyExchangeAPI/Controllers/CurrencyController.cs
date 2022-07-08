using CurrencyExchangeAPI.Models;
using CurrencyExchangeAPI.Services;
using CurrencyExchangeAPI.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchangeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
 
            _currencyService = currencyService;
        }


        [HttpGet]
        public IActionResult GetConvertCurrency([FromQuery] CurrencyViewModel currency)
        {
          
            IDataResult<Currency> result = _currencyService.ConvertCurrency(currency);

            if (result.Success)
                 return Ok(result);
            else
                return BadRequest(result.Message);
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAllCurrency()
        {
            

            string result = _currencyService.GetAllCurrency();

                return Ok(result);     
        }

        [HttpGet]
        [Route("getExchange")]
        public IActionResult GetAllCurrency([FromQuery] ExcahangeViewModel model)
        {
            IDataResult<Excahange> result = _currencyService.GetSeletedCurrency(model);

            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result.Message);
        }
    }
}
