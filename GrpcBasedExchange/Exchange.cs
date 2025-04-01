using Exchange;
using Grpc.Net.Client;
using GrpcBasedExchange;
using GrpcBasedExchange.Services;
using System;

public class ExchangeComponent : IExchange
{
    private readonly ExchangeServerWrapper _serverWrapper;
    private readonly IProcessor _processHandler;

    public ExchangeComponent(IProcessor processHandler)
    {
        _processHandler = processHandler;
        _serverWrapper = new ExchangeServerWrapper(_processHandler);
    }

    public void Start()
    {
        _serverWrapper.Start();
    }

    public void Stop()
    {
        _serverWrapper.Stop();
    }

    public string SendMessage(string sender, string target, string message)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:50052"); // Forward to Process B
        var client = new ExchangeService.ExchangeServiceClient(channel);

        var response = client.SendMessage(new MessageRequest
        {
            Sender = sender,
            Target = target,
            Message = message
        });

        return response.Reply;
    }
}
