using System;
using Grpc.Net.Client;
using GrpcDemo;
using static GrpcDemo.Greeter;

namespace GrpcDemoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");

            var client = new GreeterClient(channel);

            var reply = client.SayHello(new HelloRequest { Name = "MeltpoolMartin" });

            Console.WriteLine("Greeting: " + reply.Message);

            Console.WriteLine("Press a key to exit");
            Console.ReadKey();
        }
    }
}
