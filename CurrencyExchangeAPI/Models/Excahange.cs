namespace CurrencyExchangeAPI.Models
{
    public class Excahange
    {
        public string Base { get; set; }
        public string Date { get; set; }
        public IDictionary<string,double> Rates { get; set; }
        public bool Success { get; set; }
    }
}
