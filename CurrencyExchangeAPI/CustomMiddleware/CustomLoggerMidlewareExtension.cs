namespace CurrencyExchangeAPI.CustomMiddleware
{

    public static class CustomLoggerMidlewareExtension
    {
        public static IApplicationBuilder UseCustomeLogger(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomLoggerMidleware>();
        }
    }
}
