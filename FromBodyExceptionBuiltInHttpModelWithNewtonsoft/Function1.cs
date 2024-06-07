using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;

namespace FromBodyExceptionBuiltInHTTPModel
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
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req, [FromBody] InputDto? dto)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            _logger.LogInformation($"My name is : '{dto?.Name}'");

            var response = req.CreateResponse();

            await response.WriteAsJsonAsync(new ResponseDto
            {
                Id = Guid.NewGuid(),
                Name = dto?.Name
            });

            return response;
        }
    }
}