using Newtonsoft.Json;

namespace WhatsAppInMVC.DTOs
{
    public class UpdateMessageDTO
    {
        [JsonProperty("messaging_product")]
        public string MessagingProduct { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message_id")]
        public string MessageId { get; set; }
    }
}
