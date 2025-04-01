
using GrpcBasedExchange;
using ProcessB;

Console.WriteLine("Hello");
MYService mYService = new MYService();
mYService.RegisterClient("ProcessB",new MyDrawRectanleHandler());
Console.ReadLine();