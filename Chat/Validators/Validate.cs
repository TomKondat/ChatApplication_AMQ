
namespace Chat.Validators
{
    public class Validate
    {
        public static string UserNameValidate()
        {
            while (true)
            {
                Console.WriteLine("Please enter your username");
                var userName = Console.ReadLine();
                if (!string.IsNullOrEmpty(userName))
                {
                    return userName;
                }
                else
                {
                    Console.WriteLine("Username cannot be empty, try again");
                }
            }
        }

    }
}
