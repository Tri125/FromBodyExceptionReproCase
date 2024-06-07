using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;

namespace InProcessModel
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
        [FunctionName("Function1")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] InputDto? dto, HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"My name is : '{dto?.Name}'");

            return new JsonResult(new ResponseDto
            {
                Id = Guid.NewGuid(),
                Name = dto?.Name
            });
        }
    }
}
