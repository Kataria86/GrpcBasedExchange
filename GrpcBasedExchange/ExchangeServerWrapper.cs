using Exchange;
using Grpc.Core;
using GrpcBasedExchange;
using GrpcBasedExchange.Services;
using Microsoft.AspNetCore.Hosting.Server;
using System;
using System.Threading.Tasks;

public class ExchangeServerWrapper
{
    private readonly Server _server;

    public ExchangeServerWrapper(IProcessor processHandler, int port = 50052)
    {
        _server = new Server
        {
            Services = { ExchangeService.BindService(new ExchangeServiceImp(processHandler)) },
            Ports = { new ServerPort("localhost", port, ServerCredentials.Insecure) }
        };
    }

    public void Start()
    {
        _server.Start();
        Console.WriteLine("Exchange Server started on port 50051");
    }

    public void Stop()
    {
        _server.ShutdownAsync().Wait();
    }
}
