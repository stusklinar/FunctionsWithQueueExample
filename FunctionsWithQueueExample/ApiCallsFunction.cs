using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace FunctionsWithQueueExample
{
    public class ApiCallsFunction
    {
        private readonly ILogger _logger;

        public ApiCallsFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ApiCallsFunction>();
        }

        //This is an azure function
        [Function("ApiCallOne")]

        [ServiceBusOutput("WebhookOneQueue", Connection = "ServiceBusConnection")] // This will be a special setting defined in appsettings.json (or other cloud options are available)

        //This is a HTTP trigger so it will be triggered by... URL/ApiCallOne/?Parmams or body or whatever
        public string ApiCallOne([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("Received");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            //Stuff here to get your object from the request
            var reqObject = req.ReadFromJsonAsync<MyObject>();

            //This is the message that will go to ServiceBus (Obviously it can be json)
            var message = $"Output message created at {DateTime.Now}";
            return message;
        }

        [Function("ApiCallTwo")]

        [ServiceBusOutput("WebhookTwoQueue", Connection = "ServiceBusConnection")] // This will be a special setting defined in appsettings.json (or other cloud options are available)

        //This is a HTTP trigger so it will be triggered by... URL/ApiCallTwo/?Parmams or body or whatever
        public string ApiCallTwo([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("Received");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            //Stuff here to get your object from the request
            var reqObject = req.ReadFromJsonAsync<MyObject>();

            //This is the message that will go to ServiceBus (Obviously it can be json)
            var message = $"Output message created at {DateTime.Now}";
            return message;
        }
    }

    public class MyObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
