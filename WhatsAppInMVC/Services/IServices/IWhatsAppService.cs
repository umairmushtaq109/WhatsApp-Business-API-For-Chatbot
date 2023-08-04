
using System.Threading.Tasks;
using WhatsAppInMVC.DTOs;

namespace WhatsAppInMVC.Services.IServices
{
    public interface IWhatsAppService
    {
        Task<int> WebhookVerification(string hubMode, int hubChallenge, string hubVerifyToken);
        Task<IncomingMessageDTO> ParseMessage(string jsonResponse);
        Task<string> SendTextMessage(TextMessage textMessage);
        Task<bool> UpdateMessageStatus(UpdateMessageDTO updateMessage);
    }
}
