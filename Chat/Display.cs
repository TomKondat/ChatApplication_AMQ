
namespace Chat
{
    public static class  Display
    {
        public static void DisplayUserOptions()
        {
            Console.WriteLine();
            Console.WriteLine("Type this command /join:{chat room name} to join ongoing chat room");
            Console.WriteLine("Type this command /new:{chat room name} to open and join a new room");
            Console.WriteLine("Type this command /leave:{chat room name} to unsubscribe to messages from this room");
            Console.WriteLine("Type this command /printChatRoomMessages:{chat room name} to print all messages from a room");
            Console.WriteLine("Type this command /printPrivateChatMessages:{chat room name} to print all private messages from a room");
            Console.WriteLine("Type this command /exit to close the application");
            Console.WriteLine("Type this command {chat room name}:{msg} to send message to all participants in this room.");
            Console.WriteLine("Type this command {chat room name}:@{to user_name}:{msg} to send private message to this user.");
            Console.WriteLine();
        }
    }
}
