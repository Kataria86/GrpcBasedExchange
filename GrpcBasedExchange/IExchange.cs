namespace GrpcBasedExchange
{
    public interface IExchange
    {
        string SendMessage(string sender, string target, string message);
    }

}
