// See https://aka.ms/new-console-template for more information
using GrpcBasedExchange;

Console.WriteLine("Hello, World!");

MYService ms = new MYService();
ms.RegisterClient("ProcessA", null);

ms.SenedMessage("Hi Ankit");

while (true)
{
   var msg= Console.ReadLine();
    Console.WriteLine(DateTime.Now.Millisecond);

    ms.SenedMessage(msg);
}