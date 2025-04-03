using Exchange;
using Grpc.Net.Client;
using SDCIPCCore;

namespace ExchnageClient
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
            var call = client.Register(new ClientRegistrationRequest { ClientId = clientId });

            Console.WriteLine("Listening for messages...");
            while (await call.ResponseStream.MoveNext(CancellationToken.None))
            {
                var message = call.ResponseStream.Current;

                processorCore.Process(new MessageContainer
                {
                    MessageId = message.MessageId,
                    MessagePayload = message.MessagePayload,

                });

                //if (handle != null)
                //{
                //    var r = handle.HandleMessage("test", message.ToString());
                //    client.SendMessage(new MessageRequest { Message = r });
                //}

                Console.WriteLine($"Message from:{DateTime.Now.Microsecond} {message}");
            }
        }

        public bool SendMessage(MessageContainer message)
        {

            var request = new MessageRequest
            {
                MessageId =message.MessageId,
                //Receivers=new Google.Protobuf.Collections.RepeatedField<string>(),
                MessagePayload = message.MessagePayload,
               TransactionId="123"
            };
            request.Receivers="DeviceControl";

            var result = client.SendMessage(request);

            return result.Success;
        }
    }
}
