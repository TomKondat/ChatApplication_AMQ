namespace Chat.DTO
{
    public class ChatMsg
    {
        public string FromUserName { get; set; }
        public string ToUserName { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
