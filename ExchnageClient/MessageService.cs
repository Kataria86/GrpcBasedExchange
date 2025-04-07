using Exchange;
using Grpc.Net.Client;
using Newtonsoft.Json;
using SDCIPCCore;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ExchnageClient
{

    public class MessageService : IMessageService
    {


        private readonly ExchangeService.ExchangeServiceClient client;

        public MessageService(string clientId)
        {
            SenderId = clientId;
            var channel = GrpcChannel.ForAddress("http://localhost:5215");
            client = new ExchangeService.ExchangeServiceClient(channel);
        }

        public string SenderId { get; set; }


        public async Task RegisterClient(string clientId, MessageProcessorCore processorCore)
        {
            var call = client.RegisterClient(new ClientRegistrationRequest { ClientId = clientId });

            Console.WriteLine("Listening for messages...");
            while (await call.ResponseStream.MoveNext(CancellationToken.None))
            {
                var message = call.ResponseStream.Current;

                var processigResult = processorCore.Process(message.MessageId, message.MessagePayload);

                if (!string.IsNullOrEmpty(message.Receiver))
                {
                    await client.SendResponseAsync(
                          new MessageResponse
                          {
                              TransactionId = message.TransactionId,
                              Data = JsonConvert.SerializeObject(processigResult.Data)
                          });
                }

                Console.WriteLine($"Message from:{DateTime.Now.Microsecond} {message}");
            }
        }

        public async Task<bool> BroadcastMessage(MessageBase message)
        {
            Acknowledge result;
            MessageRequest request = new MessageRequest
            {
                MessageId = message.MessageId,
                Sender = SenderId,
                MessagePayload = JsonConvert.SerializeObject(message)
            };
            result = await client.SendMessageAsync(request);
            return result.Success;
        }


        public async Task<Response> SendMessage(string receiverId, MessageBase message)
        {
            await Task.Delay(5000);

            Acknowledge result;

            MessageRequest request = new MessageRequest
            {
                MessageId = message.MessageId,
                Sender = SenderId,
                Receiver = receiverId,
                TransactionId = Guid.NewGuid().ToString(),
                MessagePayload = JsonConvert.SerializeObject(message)
            };

            result = await client.SendMessageAsync(request);



            return new Response(result.Success, result.Reply);
        }

        public async Task<T?> SendMessage<T>(string receiverId, MessageBase message)
        {
            MessageRequest request = new MessageRequest
            {
                MessageId = message.MessageId,
                Sender = SenderId,
                Receiver = receiverId,
                TransactionId = Guid.NewGuid().ToString(),
                MessagePayload = JsonConvert.SerializeObject(message)
            };

            var result = await client.SendMessageAsync(request);

            if (result.Success)
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(result.Reply);

                }
                catch (Exception ex)
                {
                    return default;

                }

            }
            else
            {
                return default;
            }

        }



        public async Task<bool> SendResponse(string transactionId, bool success, string data)
        {

            var responMesage = new MessageResponse
            {
                TransactionId = transactionId,
                Result = success,
                Data = data
            };
            Acknowledge result = await client.SendResponseAsync(responMesage);

            return result.Success;
        }


    }
}
