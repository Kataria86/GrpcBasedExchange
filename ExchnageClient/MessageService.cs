using Exchange;
using Grpc.Net.Client;
using SDCIPCCore;

-namespace ExchnageClient
{
    public class MessageService : IMessageService
    {
        ExchangeService.ExchangeServiceClient client;

        public MessageService()
        {

            var channel = GrpcChannel.ForAddress("http://localhost:5215");
            client = new ExchangeService.ExchangeServiceClient(channel);

        }


        public async Task RegisterClient(string clientId, MessageProcessorCore processorCore)
        {   
            var call = client.Register(new ClientRegistrationRequest { ClientId=clientId});
            
            Console.WriteLine("Listening for messages...");
            while (await call.ResponseStream.MoveNext(CancellationToken.None))
            {
                var message = call.ResponseStream.Current;
                //if (handle != null)
                //{
                //    var r = handle.HandleMessage("test", message.ToString());
                //    client.SendMessage(new MessageRequest { Message = r });
                //}

                Console.WriteLine($"Message from:{DateTime.Now.Microsecond} {message.Reply}");
            }
        }

        public void SenedMessage(MessageContainer message)
        {
            client.SendMessage(new MessageRequest
            {
                MessageId = message.MessageId,
                MessagePayload = message.MessagePayload


            });
            
        }
    }
}
       