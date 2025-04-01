using Exchange;
using Grpc.Core;

namespace GrpcBasedExchange.Services
{
    public class ExchangeServiceImp : ExchangeService.ExchangeServiceBase
    {
        public static Dictionary<string, IServerStreamWriter<MessageResponse>> cleints = new Dictionary<string, IServerStreamWriter<MessageResponse>>();
        public ExchangeServiceImp()
        {
        }

        public override Task<MessageResponse> SendMessage(MessageRequest request, ServerCallContext context)
        {
            foreach (var client in cleints)
            {
                var r = client.Value.WriteAsync(new MessageResponse() { Reply = request.Message });
            }
            return Task.FromResult(new MessageResponse
            {
                Success = true,
                //Reply = responseMessage
            });
        }

        public override async Task Register(MessageRequest request, IServerStreamWriter<MessageResponse> responseStream, ServerCallContext context)
        {
            cleints[request.Target] = responseStream;
            await Task.Delay(-1);
        }

    }
}
