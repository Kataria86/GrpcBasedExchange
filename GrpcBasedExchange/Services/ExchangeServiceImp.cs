using Exchange;
using Grpc.Core;
using System.Collections.Concurrent;

namespace GrpcBasedExchange.Services
{
    public class ExchangeServiceImp : Exchange.ExchangeService.ExchangeServiceBase
    {
        private static Dictionary<string, IServerStreamWriter<MessageRequest>> cleints = new();
        private static readonly ConcurrentDictionary<string, TaskCompletionSource<MessageResponse>> _pendingResponses = new();




        public override async Task<Acknowledge> SendMessage(MessageRequest request, ServerCallContext context)
        {
            Acknowledge result = null;
            if (request.Receiver == null)//Broadcast the message
            {
                foreach (var client in cleints.Values)
                {
                    await client.WriteAsync(request);
                }

                result = await Task.FromResult(new Acknowledge
                {
                    Success = true,
                });
            }
            else// send message to specific cleint app.
            {
                var client = cleints[request.Receiver];

                if (client == null)
                {
                    //TODO need to re-think
                    return await Task.FromResult(new Acknowledge { });
                }

                var tcs = new TaskCompletionSource<MessageResponse>();
                _pendingResponses[request.TransactionId] = tcs;

                await client.WriteAsync(request);

                var response = await tcs.Task;

                result = await Task.FromResult(new Acknowledge
                {
                    Success = true,
                    Reply = response.Data
                });

            }

            return result;
        }

        public override async Task<Acknowledge> SendResponse(MessageResponse request, ServerCallContext context)
        {
            if (_pendingResponses.TryRemove(request.TransactionId, out var tcs))
            {
                tcs.SetResult(request); // Unblock the waiting client
            }

            return await Task.FromResult(new Acknowledge { Success = true });
        }

        public override async Task RegisterClient(ClientRegistrationRequest request, IServerStreamWriter<MessageRequest> responseStream, ServerCallContext context)
        {
            cleints[request.ClientId] = responseStream;

            await Task.Delay(-1);
        }

    }
}
