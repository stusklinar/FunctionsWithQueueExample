using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Consumer
{
    public class ConsumerFunction
    {
        private readonly ILogger _logger;

        public ConsumerFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ConsumerFunction>();
        }

        [Function("ServiceBusProcessor")]

        //A service bus trigger - this will trigger the function to run whenever a message is put on the queue

        //Using this approach you could have many functions to process each different scenarios
        public void WebhookOneQueue([ServiceBusTrigger("WebhookOneQueue", Connection = "ConnectionStringrSettingName")] string myQueueItem)
        {
            _logger.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
            //Consumer Code
        }

        public void WebhookTwoQueue([ServiceBusTrigger("WebhookTwoQueue", Connection = "ConnectionStringrSettingName")] string myQueueItem)
        {
            _logger.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
            //Consumer Code
        }
    }
}
