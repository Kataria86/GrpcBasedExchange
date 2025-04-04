using Exchange;
using Grpc.Net.Client;

namespace GrpcBasedExchange
{
    public class MYService
    {
        ExchangeService.ExchangeServiceClient client;
        public MYService()
        {

            var channel = GrpcChannel.ForAddress("http://localhost:5215");
            client = new ExchangeService.ExchangeServiceClient(channel);

        }

        public async void RegisterClient(string clientId, IProcessor handle)
        {
            

            //var request = new ReceiveRequest { ClientName = "ClientB" };

            var call = client.Register(new MessageRequest { Message = "Test1", Target=clientId });
            //using var call = client.ReceiveMessages(request);

            Console.WriteLine("Listening for messages...");
            while (await call.ResponseStream.MoveNext(CancellationToken.None))
            {
                var message = call.ResponseStream.Current;
                if (handle!=null)
                {
                    var r = handle.HandleMessage("test", message.ToString());
                    if (message.) {
                    client.(new MessageRequest { Message = r });
                }


                Console.WriteLine($"Message from:{DateTime.Now.Microsecond} {message.Reply}");
            }
        }

        public void SenedMessage(string message)
        {

            //var request = new ReceiveRequest { ClientName = "ClientB" };

            client.SendMessage(new MessageRequest { Message = message });
        }
    }
}
