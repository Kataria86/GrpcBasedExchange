using Exchange;
using Grpc.Net.Client;
using SDCIPCCore;
using System.Threading.Tasks;

namespace ExchnageClient
{

    public class MessageService : IMessageService
    {

        ExchangeService.ExchangeServiceClient client;
        ManualResetEvent ManualResetEvent = new ManualResetEvent(false);

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

                if (message.WaitingForResponse)
                {
                    client.SendResponse(
                        new MessageRequest
                        {
                            MessageId = message.MessageId,
                            Receivers = message.Sender,
                            TransactionId = message.TransactionId,
                            MessagePayload = "Hi Here is my Response"
                        }

                        );
                }
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
                MessageId = message.MessageId,
                //Receivers=new Google.Protobuf.Collections.RepeatedField<string>(),
                MessagePayload = message.MessagePayload,
                TransactionId = "123",
            };
            request.Receivers = "DeviceControl";

            var result = client.SendMessage(request);
          

            return result.Success;
        }

        public async Task<bool> SendMessageWithWait(MessageContainer message)
        {
            string transactionId = Guid.NewGuid().ToString();


            var request = new MessageRequest
            {
                MessageId = message.MessageId,
                //Receivers=new Google.Protobuf.Collections.RepeatedField<string>(),
                MessagePayload = message.MessagePayload,
                TransactionId = transactionId,
                WaitingForResponse = true
            };

            var responseTask = client.WaitForResponseAsync(new RequestId { TransactionIdId = transactionId });

            request.Receivers = "DeviceControl";

            var awk = client.SendMessage(request);


            var result = await responseTask;

            return true;
        }

        
    }
}
