
using Chat.DTO;
using DataScope.Amq.Client;
using Newtonsoft.Json;

namespace Chat.Classes
{
    public class InputHandler
    {
        private IBroker _broker;
        private List<ChatRoom> _chatRooms;
        public User CurrentUser { get; }

        public InputHandler(IBroker broker, User user)
        {
            _broker = broker;
            CurrentUser = user;
            _chatRooms = new List<ChatRoom>();
        }
        public void ProcessInput(string input)
        {

            var commandArgs = input.Split(':');
            if(commandArgs.Length < 2)
            {
                Console.WriteLine("Invalid command. Please try again.");
                return;
            }

            if (input.StartsWith("/"))
            {
                switch (commandArgs[0])
                {
                    case "/join":
                        JoinChatRoom(commandArgs[1]);
                        break;
                    case "/leave":
                        LeaveChatRoom(commandArgs[1]);
                        break;
                    case "/new":
                        JoinChatRoom(commandArgs[1]);
                        break;
                    case "/printChatRoomMessages":
                        printChatRoomMessages(commandArgs[1]);
                        break;
                    case "/printPrivateChatMessages":
                        printPrivateChatMessages(commandArgs[1]);
                        break;
                    case "/exit":
                        _broker.Disconnect();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid command. Please try again.");
                        break;
                }
            }
            else if (commandArgs[1].StartsWith("@"))
            {
                SendPrivateChatMessage(commandArgs[0], commandArgs[1], commandArgs[2]);
            }
            else
            {
                SendChatMessage(commandArgs[0], commandArgs[1]);
            }
        }
        private void JoinChatRoom(string roomName)
        {
            if (_chatRooms.Any(x => x.RoomName == roomName))
            {
                Console.WriteLine($"{CurrentUser.UserName} is already in chat room '{roomName}'.");
            }
            else
            {
                var chatRoom = new ChatRoom(roomName, _broker, CurrentUser.UserName);
                _chatRooms.Add(chatRoom);
                Console.WriteLine($"{CurrentUser.UserName} joined chat room '{roomName}'.");
            }
        }
        private void LeaveChatRoom(string roomName)
        {
            _chatRooms.RemoveAll(x => x.RoomName == roomName);
            Console.WriteLine($"{CurrentUser.UserName} left chat room '{roomName}'.");
        }
        private void printChatRoomMessages(string roomName)
        {
            var room = _chatRooms.FirstOrDefault(x => x.RoomName == roomName);
            if (room == null)
            {
                Console.WriteLine($"You are not in chat room '{roomName}'.");
                return;
            }
            room.PrintChatRoomMessages();
        }
        private void printPrivateChatMessages(string roomName)
        {
            var room = _chatRooms.FirstOrDefault(x => x.RoomName == roomName);
            if (room == null)
            {
                Console.WriteLine($"You are not in chat room '{roomName}'.");
                return;
            }
            room.PrintPrivateChatMessages();
        }
        private void SendChatMessage(string roomName, string msg)
        {
            var room = _chatRooms.FirstOrDefault(x => x.RoomName == roomName);
            if (room == null)
            {
                Console.WriteLine($"You are not in chat room '{roomName}'.");
                return;
            }
            var chatMsg = new ChatMsg()
            {
                Content = msg,
                FromUserName = CurrentUser.UserName,
                Timestamp = DateTime.Now
            };
            string value = JsonConvert.SerializeObject(chatMsg);
            room.Producer.Send(new Message() { Text = value });
        }
        private void SendPrivateChatMessage(string roomName, string toUserName, string msg)
        {
            var room = _chatRooms.FirstOrDefault(x => x.RoomName == roomName);
            if (room == null)
            {
                Console.WriteLine($"You are not in chat room '{roomName}'.");
                return;
            }
            var chatMsg = new ChatMsg()
            {
                Content = msg,
                FromUserName = CurrentUser.UserName,
                ToUserName = toUserName.Substring(1),
                Timestamp = DateTime.Now
            };
            string value = JsonConvert.SerializeObject(chatMsg);
            room.Producer.Send(new Message() { Text = value });
        }
    }
}
