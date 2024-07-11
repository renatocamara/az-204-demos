using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class HttpTrigger1
    {
        private readonly ILogger<HttpTrigger1> _logger;

        public HttpTrigger1(ILogger<HttpTrigger1> logger)
        {
            _logger = logger;
        }

        [Function("HttpTrigger1")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            var secret01 = Environment.GetEnvironmentVariable("secret01");
            _logger.LogInformation($"Secret01: {secret01}");
            
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
