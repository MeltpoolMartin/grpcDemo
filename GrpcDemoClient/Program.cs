using System;
using System.Net.Http;
using Grpc.Net.Client;
using GrpcDemo;
using static GrpcDemo.Greeter;

namespace GrpcDemoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://docs.microsoft.com/en-US/aspnet/core/grpc/troubleshoot?view=aspnetcore-5.0
            var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var channel = GrpcChannel.ForAddress("https://localhost:5001",
                new GrpcChannelOptions { HttpHandler = httpHandler });


            var client = new GreeterClient(channel);

            var reply = client.SayHello(new HelloRequest { Name = "MeltpoolMartin" });

            Console.WriteLine("Greeting: " + reply.Message);

            Console.WriteLine("Press a key to exit");
            Console.ReadKey();
        }
    }
}
