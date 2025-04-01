
using GrpcBasedExchange;

Console.WriteLine("Hello");
MYService mYService = new MYService();
mYService.RegisterClient("ProcessC");
Console.ReadLine();