using System;
using System.Net.Http;
using Grpc.Net.Client;
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
            var channel = GrpcChannel.ForAddress("http://localhost:5005");

            var client = new RandomNumberClient(channel);

            var reply = client.GetRandomInteger(new RandomIntegerRequest { LowerLimit = 0, UpperLimit = 10 });

            Console.WriteLine("Random number: " + reply.RandomInteger);

            Console.WriteLine("Press a key to exit");
            Console.ReadKey();
        }
    }
}
