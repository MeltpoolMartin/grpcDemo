using System;
using System.Net.Http;
using GrpcDemo;
using static GrpcDemo.RandomNumber;

namespace GrpcDemoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Workaround for development HTTPS
            // https://docs.microsoft.com/en-US/aspnet/core/grpc/troubleshoot?view=aspnetcore-5.0
            // var httpHandler = new HttpClientHandler();
            // // Return `true` to allow certificates that are untrusted/invalid
            // httpHandler.ServerCertificateCustomValidationCallback =
            //     HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            // var channel = GrpcChannel.ForAddress("http://localhost:5000",
            //     new GrpcChannelOptions { HttpHandler = httpHandler });

            // Plain HTTP connection
            var endpoint = new RandomNumberEndpoint("127.0.0.1:5005");

            // Console.WriteLine("Random number: " + endpoint.GetRandomInteger(0, 10));
            endpoint.GetRandomIntegerStream(lowerLimit: 0, upperLimit: 10, duration: 5);

            Console.WriteLine("Press a key to exit");
            Console.ReadKey();
        }
    }
}
