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

        private Task<RandomIntegerReply> GenerateRandomNumber(RandomIntegerRequest request)
        {
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

        public override Task<RandomIntegerReply> GetRandomInteger(RandomIntegerRequest request, ServerCallContext context)
        {
            _logger.LogDebug($"Client {context.Host} requests method {context.Method}");
            return GenerateRandomNumber(request);
        }

        public override async Task GetRandomIntegerStream(RandomIntegerRequest request, IServerStreamWriter<RandomIntegerReply> responseStream, ServerCallContext context)
        {
            while (context.CancellationToken.IsCancellationRequested != true)
            {
                await responseStream.WriteAsync(GenerateRandomNumber(request).Result);
                // Make delay configurable
                await Task.Delay(100);
            }
        }
    }
}
