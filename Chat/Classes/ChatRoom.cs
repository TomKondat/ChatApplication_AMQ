
using Chat.DTO;
using DataScope.Amq.Client;
using Newtonsoft.Json;

namespace Chat.Classes
{
    public class ChatRoom
    {
        public string RoomName { get; }
        public string UserName { get; }
        public List<ChatMsg> Messages { get; }
        public IConsumer Consumer { get; }
        public IProducer Producer { get; }

        public ChatRoom(string roomName, IBroker broker, string userName)
        {
            RoomName = roomName;
            UserName = userName;
            Messages = new List<ChatMsg>();
            Consumer = broker.Consumers.ForTopic($"ChatApp.{roomName}");
            Producer = broker.Producers.ForTopic($"ChatApp.{roomName}");
            Consumer.OnMessageReceived += HandleMessage;
        }
        public void HandleMessage(Message msg)
        {
            var chatMsg = JsonConvert.DeserializeObject<ChatMsg>(msg.Text);
            if (chatMsg.ToUserName != null)
            {
                if (chatMsg.ToUserName == UserName)
                {
                    Messages.Add(chatMsg);
                    Console.WriteLine($"Received private message '{chatMsg.Content}' from {chatMsg.FromUserName}");
                }
            }
            else
            {
                Messages.Add(chatMsg);
                Console.WriteLine($"Received message '{chatMsg.Content}' from {chatMsg.FromUserName}");
            }
        }
        public void PrintChatRoomMessages()
        {
            foreach (var msg in Messages)
            {
                if (msg.ToUserName == null)
                {
                    Console.WriteLine($"{msg.FromUserName}: {msg.Content}");
                }
            }
        }
        public void PrintPrivateChatMessages()
        {
            foreach (var msg in Messages)
            {
                if (msg.ToUserName != null)
                {
                    Console.WriteLine($"{msg.FromUserName}: {msg.Content}");
                }
            }
        }
        public void Dispose()
        {
            Consumer.Dispose();
            Producer.Dispose();
        }
    }
}
