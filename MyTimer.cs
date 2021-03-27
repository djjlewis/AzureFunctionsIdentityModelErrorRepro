using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsWithIdentityModelErrorRepro
{
    public class MyTimer
    {
        private readonly ILogger<MyTimer> _logger;

        public MyTimer(ILogger<MyTimer> logger)
        {
            _logger = logger;
        }
        
        [Function("MyTimer")]
        public void Run([TimerTrigger("*/15 * * * * *")] MyInfo myTimer, FunctionContext context)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
        }
    }

    public class MyInfo
    {
        public MyScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
