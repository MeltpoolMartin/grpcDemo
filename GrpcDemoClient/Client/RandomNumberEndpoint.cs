using System.Threading;
using Grpc.Core;
using GrpcDemo;
using System.Threading.Tasks;

namespace GrpcDemoClient
{
    public class RandomNumberEndpoint
    {
        readonly RandomNumber.RandomNumberClient _client;
        public RandomNumberEndpoint(string ipAddress)
        {
            var channel = new Channel(ipAddress, ChannelCredentials.Insecure);
            _client = new RandomNumber.RandomNumberClient(channel);
        }

        public int GetRandomInteger(int lowerLimit, int upperLimit)
        {
            return _client.GetRandomInteger(new RandomIntegerRequest { LowerLimit = lowerLimit, UpperLimit = upperLimit })
                .RandomInteger;
        }

        public async void GetRandomIntegerStream(int lowerLimit, int upperLimit, int duration)
        {
            var request = new RandomIntegerRequest
            {
                LowerLimit = lowerLimit,
                UpperLimit = upperLimit
            };


            using (var call = _client.GetRandomIntegerStream(request))
            {
                var count = 0;
                var responseStream = call.ResponseStream;
                var tokenSource = new CancellationTokenSource();
                try
                {
                    while (await responseStream.MoveNext(tokenSource.Token))
                    {
                        if (count < duration)
                        {
                            var randomNumber = responseStream.Current;
                            System.Console.WriteLine(randomNumber);
                            count++;
                        }
                        else
                        {
                            System.Console.WriteLine($"Request cancelation after {count} iterations");
                            tokenSource.Cancel();
                        }
                    }
                }
                catch (RpcException exec) when (exec.Status.StatusCode == StatusCode.Cancelled)
                {
                    System.Console.WriteLine($"Streaming was canceld from client");
                }
            }
        }
    }
}
