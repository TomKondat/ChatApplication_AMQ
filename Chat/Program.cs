
using Chat.Classes;
using DataScope.Amq.Client;

class Program
{
    static void Main(String[] args)
    {
        //-------------------change the connection string
        var brokerConnStr = "failover:(tcp://localhost:61616,tcp://localhost:61616)";
        IBroker _broker = new Broker(brokerConnStr, "admin", "admin");

        Console.WriteLine("Welcome to Chat App!");

        var user = new User();

        var inputHandler = new InputHandler(_broker, user);

        while (true)
        {
            var input = Console.ReadLine();
            inputHandler.ProcessInput(input);
        }
    }
}

