using WhatsAppInMVC.DTOs;

namespace WhatsAppInMVC.Services.IServices
{
    public interface IMessageBuilderService
    {
        TextMessage BuildTextMessage(string phone, string message);
    }
}
