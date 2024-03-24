
using Chat.Validators;

namespace Chat.Classes
{
    public class User
    {
        public string UserName { get; }

        public List<ChatRoom> JoinedRooms { get; }

        public User()
        {
            UserName = Validate.UserNameValidate();
            Console.WriteLine($"Welcome to chat app {UserName}");
            Display.DisplayUserOptions();
        }

        public void JoinRoom(ChatRoom room)
        {
            JoinedRooms.Add(room);
            Console.WriteLine($"{UserName} joined the chat room: {room.RoomName}");
        }

        public void LeaveRoom(ChatRoom room)
        {
            JoinedRooms.Remove(room);
            Console.WriteLine($"{UserName} left the chat room: {room.RoomName}");
        }
    }
}
