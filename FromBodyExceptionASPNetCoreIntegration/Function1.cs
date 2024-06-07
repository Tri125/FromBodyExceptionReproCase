using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;

namespace FromBodyException
{
    public class InputDto
    {
        public string? Name { get; set; }
    }

    public class ResponseDto
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }
    }

    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function("Function1")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req, [FromBody] InputDto? dto)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            _logger.LogInformation($"My name is : '{dto?.Name}'");

            return new JsonResult(new ResponseDto
            {
                Id = Guid.NewGuid(),
                Name = dto?.Name
            });
        }
    }
}
