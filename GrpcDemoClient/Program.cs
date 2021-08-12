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
            // Workaround for development HTTPS
            // https://docs.microsoft.com/en-US/aspnet/core/grpc/troubleshoot?view=aspnetcore-5.0
            // var httpHandler = new HttpClientHandler();
            // // Return `true` to allow certificates that are untrusted/invalid
            // httpHandler.ServerCertificateCustomValidationCallback =
            //     HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            // var channel = GrpcChannel.ForAddress("http://localhost:5000",
            //     new GrpcChannelOptions { HttpHandler = httpHandler });

            // Plain HTTP connection
            var channel = GrpcChannel.ForAddress("http://localhost:5000");

            var client = new GreeterClient(channel);

            var reply = client.SayHello(new HelloRequest { Name = "MeltpoolMartin" });

            Console.WriteLine("Greeting: " + reply.Message);

            Console.WriteLine("Press a key to exit");
            Console.ReadKey();
        }
    }
}
