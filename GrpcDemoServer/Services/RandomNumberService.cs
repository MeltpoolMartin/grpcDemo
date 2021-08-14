using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcDemo
{
    public class RandomNumberService : RandomNumber.RandomNumberBase
    {
        private readonly ILogger<RandomNumberService> _logger;
        private Random _random;

        public RandomNumberService(ILogger<RandomNumberService> logger)
        {
            _logger = logger;
            _random = new Random();
        }

        public override Task<RandomIntegerReply> GetRandomInteger(RandomIntegerRequest request, ServerCallContext context)
        {
            _logger.LogDebug($"Client {context.Host} requests method {context.Method}");
            if (request.LowerLimit > request.UpperLimit)
            {
                _logger.LogWarning($"LowerLimit {request.LowerLimit} is greater as {request.UpperLimit}");
                return Task.FromResult(new RandomIntegerReply { RandomInteger = -1 });
            }
            if (request.LowerLimit < 0)
            {
                _logger.LogWarning($"LowerLimit {request.LowerLimit} is negative");
                return Task.FromResult(new RandomIntegerReply { RandomInteger = -1 });
            }
            if (request.UpperLimit < 0)
            {
                _logger.LogWarning($"UpperLimit {request.UpperLimit} is negative");
                return Task.FromResult(new RandomIntegerReply { RandomInteger = -1 });
            }

            return Task.FromResult(new RandomIntegerReply
            {
                RandomInteger = _random.Next(request.LowerLimit, request.UpperLimit)
            });
        }
    }
}
