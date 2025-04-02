using Exchange;
using Grpc.Core;

namespace GrpcBasedExchange.Services
{
    public class ExchangeServiceImp : Exchange.ExchangeService.ExchangeServiceBase
    {
        private static Dictionary<string, IServerStreamWriter<MessageRequest>> cleints = new Dictionary<string, IServerStreamWriter<MessageRequest>>();
        public ExchangeServiceImp()
        {
        }

        public override Task<MessageResponse> SendMessage(MessageRequest request, ServerCallContext context)
        {
            foreach (var client in cleints)
            {
                client.Value.WriteAsync(request);
            }
            return Task.FromResult(new MessageResponse
            {
                Success = true,
                //Reply = responseMessage
            });
        }

        public override async Task Register(ClientRegistrationRequest request, IServerStreamWriter<MessageRequest> responseStream, ServerCallContext context)
        {
            cleints[request.ClientId] = responseStream;

            await Task.Delay(-1);
        }

    }
}
