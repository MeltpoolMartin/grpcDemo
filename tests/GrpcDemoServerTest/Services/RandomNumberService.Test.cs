using System;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GrpcDemo.Tests
{
    public class RandomNumberServiceTests
    {
        private readonly RandomNumberService _service;
        private Mock<ServerCallContext> _context;

        public RandomNumberServiceTests()
        {
            var logger = new Mock<ILogger<RandomNumberService>>();
            _service = new RandomNumberService(logger.Object);
            _context = new Mock<ServerCallContext>();
        }

        [Theory]
        [InlineData(1, 5)]
        [InlineData(2, 2)]
        public void GetRandomInteger_ValidRange_ReturnsRandomInteger(int lowerLimit, int upperLimit)
        {
            // Arrange
            var request = new RandomIntegerRequest { LowerLimit = lowerLimit, UpperLimit = upperLimit };

            // Act
            var reply = _service.GetRandomInteger(request, _context.Object);

            // Assert
            var randomInteger = reply.Result.RandomInteger;
            Assert.InRange<Int32>(randomInteger, lowerLimit, upperLimit);
        }

        [Theory]
        [InlineData(5, 1)]
        [InlineData(-1, -5)]
        public void GetRandomInteger_LowerLimitIsGreaterThanUpperLimit_ReturnsMinusOne(int lowerLimit, int upperLimit)
        {
            // Arrange
            var request = new RandomIntegerRequest { LowerLimit = lowerLimit, UpperLimit = upperLimit };

            // Act
            var reply = _service.GetRandomInteger(request, _context.Object);

            // Assert
            var randomInteger = reply.Result.RandomInteger;
            Assert.Equal(-1, randomInteger);
        }

        [Theory]
        [InlineData(-1, 5)]
        [InlineData(1, -5)]
        [InlineData(-5, -1)]
        public void GetRandomInteger_NegativeLimits_ReturnsMinusOne(int lowerLimit, int upperLimit)
        {
            // Arrange
            var request = new RandomIntegerRequest { LowerLimit = lowerLimit, UpperLimit = upperLimit };

            // Act
            var reply = _service.GetRandomInteger(request, _context.Object);

            // Assert
            var randomInteger = reply.Result.RandomInteger;
            Assert.Equal(-1, randomInteger);
        }
    }
}
