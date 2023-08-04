using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WhatsAppInMVC.DTOs
{
    public class MessageBase
    {
        [JsonProperty("messaging_product")]
        public string MessagingProduct { get; set; }

        [JsonProperty("recipient_type")]
        public string RecipientType { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class TextMessage : MessageBase
    {
        [JsonProperty("text")]
        public TextMessageContent Text { get; set; } = new TextMessageContent();
    }

    public class TextMessageContent
    {
        [JsonProperty("preview_url")]
        public bool PreviewUrl { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }
    }
}
