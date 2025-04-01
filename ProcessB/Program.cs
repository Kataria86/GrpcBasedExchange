
using GrpcBasedExchange;

Console.WriteLine("Hello");
MYService mYService = new MYService();
mYService.RegisterClient("ProcessB");
Console.ReadLine();