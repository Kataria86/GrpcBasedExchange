namespace GrpcBasedExchange
{
    public interface IProcessor
    {
        string HandleMessage(string sender, string message);
    }

}
