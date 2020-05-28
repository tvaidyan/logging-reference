using LoggingReference.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace LoggingReference.Web.Controllers
{
    [ResponseCache(NoStore = true, Duration = 0)]
    [Produces("application/json")]
    [Route("api/hello-world")]
    [ServiceFilter(typeof(ApiExceptionFilter))]
    public class HelloWorldController
    {
        private readonly ILogger<HelloWorldController> logger;

        public HelloWorldController(ILogger<HelloWorldController> logger)
        {
            this.logger = logger;
        }

        // GET api/hello-world/generate-log/abc123
        [HttpGet("generate-log/{testInput}")]
        public IActionResult GenerateLog(string testInput)
        {
            logger.LogTrace($"Testing trace log entry: {testInput}");
            logger.LogDebug($"Testing debug log entry: {testInput}");
            logger.LogInformation($"Testing information log entry: {testInput}");
            logger.LogWarning($"Testing warning log entry: {testInput}");
            logger.LogError($"Testing error log entry: {testInput}");
            logger.LogCritical($"Testing critical log entry: {testInput}");

            return new OkObjectResult($"Hello World.  testInput: {testInput}");
        }

        // GET api/hello-world/generate-error/abc123
        [HttpGet("generate-error/{testInput}")]
        public IActionResult GenerateError(string testInput)
        {
            throw new Exception($"Uh-oh: {testInput}");
        }
    }
}
