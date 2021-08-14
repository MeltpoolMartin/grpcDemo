using System;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GrpcDemo.Tests
{
    public class RandomNumberServiceTests
    {
        [Fact]
        public void GetRandomInteger_ValidRange_ReturnsRandomInteger()
        {
            // Arrange
            var logger = new Mock<ILogger<RandomNumberService>>();
            var service = new RandomNumberService(logger.Object);
            var lowerLimit = 1;
            var upperLimit = 5;
            var request = new RandomIntegerRequest { LowerLimit = lowerLimit, UpperLimit = upperLimit };
            var context = new Mock<ServerCallContext>();

            // Act
            var reply = service.GetRandomInteger(request: request, context: context.Object);

            // Assert
            var randomInteger = reply.Result.RandomInteger;
            Assert.InRange<Int32>(randomInteger, lowerLimit, upperLimit);
        }
    }
}
