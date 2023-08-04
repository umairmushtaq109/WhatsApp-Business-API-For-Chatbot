namespace WhatsAppInMVC.DTOs
{
    public class IncomingMessageDTO
    {
        public string Id { get; set; } = null;
        public string MessagingProduct { get; set; } = null;
        public string ChatBotDisplayPhoneNumber { get; set; } = null;
        public string ChatbotPhoneNumberId { get; set; } = null;
        public string ReceiverName { get; set; } = null;
        public string ReceiverPhoneNumber { get; set; } = null;
        public Message Message { get; set; } = new Message();
        public MessageStatus Status { get; set; } = new MessageStatus();
    }

    public class Message
    {
        public string Id { get; set; }
        public string From { get; set; }
        public string Text { get; set; }
        public string TimeStamp { get; set; }
        public string Type { get; set; }
    }

    public class MessageStatus
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string RecipientId { get; set; }
        public string TimeStamp { get; set; }
        public Conversation Conversation { get; set; } = new Conversation();
    }

    public class Conversation
    {
        public string Id { get; set; }
        public string ExpirationTimeStamp { get; set; }
    }
}
