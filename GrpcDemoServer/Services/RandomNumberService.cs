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
            return Task.FromResult(new RandomIntegerReply
            {
                RandomInteger = _random.Next(request.LowerLimit, request.UpperLimit)
            });
        }
    }
}
