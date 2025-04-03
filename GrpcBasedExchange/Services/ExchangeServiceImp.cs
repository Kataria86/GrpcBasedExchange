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

        public override async Task<MessageResponse> SendMessage(MessageRequest request, ServerCallContext context)
        {
            var client = cleints[request.Receivers];

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

    }
}
