using Exchange;
using Grpc.Core;
using System.Collections.Concurrent;

namespace GrpcBasedExchange.Services
{
    public class ExchangeServiceImp : Exchange.ExchangeService.ExchangeServiceBase
    {
        private static Dictionary<string, IServerStreamWriter<MessageRequest>> cleints = new Dictionary<string, IServerStreamWriter<MessageRequest>>();
        private static readonly ConcurrentDictionary<string, TaskCompletionSource<MessageRequest>> _pendingResponses = new();

        public ExchangeServiceImp()
        {
        }

        public override async Task<MessageResponse> SendResponse(MessageRequest request, ServerCallContext context)
        {
            //Console.WriteLine($"Received response from {request.Sender} for {request.Recipient}");

            if (_pendingResponses.TryRemove(request.TransactionId, out var tcs))
            {
                tcs.SetResult(request); // Unblock the waiting client
            }

            return new MessageResponse { Success = true };
        }

        public override async Task<MessageResponse> SendMessage(MessageRequest request, ServerCallContext context)
        {
            var client = cleints[request.Receivers];
            if (request.WaitingForResponse)
            {
                // Store a TaskCompletionSource for the response
                var tcs = new TaskCompletionSource<MessageRequest>();
                _pendingResponses[request.TransactionId] = tcs; // Use a unique messageId instead of text in real cases

            }

            if (client != null)
            {
                await client.WriteAsync(request);
            }


            var r = await Task.FromResult(new MessageResponse
            {
                Success = true,
                //Reply = responseMessage
            });

            return r;
        }

        public override async Task Register(ClientRegistrationRequest request, IServerStreamWriter<MessageRequest> responseStream, ServerCallContext context)
        {
            cleints[request.ClientId] = responseStream;

            await Task.Delay(-1);
        }

        public override async Task<MessageRequest> WaitForResponse(RequestId request, ServerCallContext context)
        {
         
            if (_pendingResponses.TryGetValue(request.TransactionIdId, out var tcs))
            {
                return await tcs.Task;
            }
            return null;
        }
    }
}
