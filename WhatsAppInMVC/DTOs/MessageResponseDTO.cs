using Newtonsoft.Json;
using System.Collections.Generic;

namespace WhatsAppInMVC.DTOs
{
    public class MessageResponseDTO
    {
        [JsonProperty("messaging_product")]
        public string MessagingProduct { get; set; }
        
        [JsonProperty("contacts")]
        public List<Contact> Contacts { get; set; }

        [JsonProperty("messages")]
        public List<Msg> Messages { get; set; }
    }

    public class Contact
    {
        [JsonProperty("profile")]
        public Profile Profile { get; set; }

        [JsonProperty("wa_id")]
        public string WAId { get; set; }
    }

    public class Msg
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("timestamp")]
        public string TimeStamp { get; set; }

        [JsonProperty("text")]
        public Text Text { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Text
    {
        [JsonProperty("body")]
        public string Body { get; set; }
    }

    public class Profile
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
