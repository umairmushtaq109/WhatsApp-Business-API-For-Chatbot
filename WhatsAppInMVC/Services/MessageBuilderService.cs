using WhatsAppInMVC.DTOs;
using WhatsAppInMVC.Services.IServices;

namespace WhatsAppInMVC.Services
{
    public class MessageBuilderService : IMessageBuilderService
    {
        public TextMessage BuildTextMessage(string phone, string message)
        {
            TextMessage textMessage = new TextMessage();
            textMessage.MessagingProduct = "whatsapp";
            textMessage.RecipientType = "individual";
            textMessage.To = "+" + phone;
            textMessage.Type = "text";
            textMessage.Text.Body = message;
            textMessage.Text.PreviewUrl = true;

            return textMessage;
        }
    }
}
