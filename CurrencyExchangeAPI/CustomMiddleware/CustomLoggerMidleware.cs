using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace CurrencyExchangeAPI.CustomMiddleware
{
    public class CustomLoggerMidleware
    {
        private readonly RequestDelegate _next;

        public CustomLoggerMidleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext httpContext)
        {
            Stopwatch watch = Stopwatch.StartNew();
            try
            {
                string message = "[Request] " + "   HTTP " + httpContext.Request.Method + " - " + httpContext.Request.Path;
                Console.WriteLine(message);

                await _next(httpContext);
                watch.Stop();

                message = "[Responce] " + " HTTP " + httpContext.Request.Method + " - " + httpContext.Request.Path + " Responced " + httpContext.Response.StatusCode + " in " + watch.Elapsed.Milliseconds + "ms";
                Console.WriteLine(message);
            }
            catch (Exception ex)
            {

                watch.Stop();
                await HandleExeption(httpContext,ex,watch);
            }
        }

        public Task HandleExeption(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "[Error]   HTTP " + context.Request.Method + " - " + context.Response.StatusCode + " Error Message " + ex.Message + " in " + watch.Elapsed.Milliseconds;
            Console.WriteLine(message);

            string result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);

            return context.Response.WriteAsync(result);
        }

    }
}
