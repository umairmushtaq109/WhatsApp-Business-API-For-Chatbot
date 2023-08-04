using Newtonsoft.Json;
using RestSharp;
using System;
using System.Configuration;
using System.Text.Json;
using System.Threading.Tasks;
using WhatsAppInMVC.DTOs;
using WhatsAppInMVC.Services.IServices;

namespace WhatsAppInMVC.Services
{
    public class WhatsAppService : IWhatsAppService
    {
        public WhatsAppService()
        {
        }

        public async Task<int> WebhookVerification(string hubMode, int hubChallenge, string hubVerifyToken)
        {
            if (!hubVerifyToken.Equals(ConfigurationManager.AppSettings["WebHookVerificationToken"]))
            {
                return -1;
            }
            return hubChallenge;
        }

        public async Task<IncomingMessageDTO> ParseMessage(string jsonResponse)
        {
            JsonDocument jsonDocument = JsonDocument.Parse(jsonResponse.ToString());

            JsonElement root = jsonDocument.RootElement;
            JsonElement entry = root.GetProperty("entry")[0];
            JsonElement changes = entry.GetProperty("changes")[0];
            JsonElement value = changes.GetProperty("value");

            IncomingMessageDTO MessageObj = new IncomingMessageDTO();

            MessageObj.MessagingProduct = value.GetProperty("messaging_product").GetString();
            MessageObj.ChatBotDisplayPhoneNumber = value.GetProperty("metadata").GetProperty("display_phone_number").GetString();
            MessageObj.ChatbotPhoneNumberId = value.GetProperty("metadata").GetProperty("phone_number_id").GetString();

            if (value.TryGetProperty("contacts", out _))
            {
                MessageObj.ReceiverName = value.GetProperty("contacts")[0].GetProperty("profile").GetProperty("name").GetString();
                MessageObj.ReceiverPhoneNumber = value.GetProperty("contacts")[0].GetProperty("wa_id").GetString();
            }

            if (value.TryGetProperty("messages", out _))
            {
                MessageObj.Message.From = value.GetProperty("messages")[0].GetProperty("from").GetString();
                MessageObj.Message.Id = value.GetProperty("messages")[0].GetProperty("id").GetString();
                MessageObj.Message.TimeStamp = value.GetProperty("messages")[0].GetProperty("timestamp").GetString();
                MessageObj.Message.Text = value.GetProperty("messages")[0].GetProperty("text").GetProperty("body").GetString();
                MessageObj.Message.Type = value.GetProperty("messages")[0].GetProperty("type").GetString();
            }

            if (value.TryGetProperty("statuses", out _))
            {
                MessageObj.Status.Id = value.GetProperty("statuses")[0].GetProperty("id").GetString();
                MessageObj.Status.Status = value.GetProperty("statuses")[0].GetProperty("status").GetString();
                MessageObj.Status.TimeStamp = value.GetProperty("statuses")[0].GetProperty("timestamp").GetString();
                MessageObj.Status.RecipientId = value.GetProperty("statuses")[0].GetProperty("recipient_id").GetString();

            }

            if (value.TryGetProperty("conversation", out _))
            {
                MessageObj.Status.Conversation.Id = value.GetProperty("statuses")[0].GetProperty("conversation").GetProperty("id").GetString();
                MessageObj.Status.Conversation.ExpirationTimeStamp = value.GetProperty("statuses")[0].GetProperty("conversation").GetProperty("expiration_timestamp").GetString();
            }
            return MessageObj;
        }

        public async Task<string> SendTextMessage(TextMessage textMessage)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"]);
            string bearerToken = ConfigurationManager.AppSettings["Token"];
            var request = new RestRequest($"/{ConfigurationManager.AppSettings["PhoneNumberID"]}/messages", Method.Post);
            request.AddHeader("Authorization", "Bearer " + bearerToken);

            string requestBody = JsonConvert.SerializeObject(textMessage);
            request.AddParameter("application/json", requestBody, ParameterType.RequestBody);
            RestResponse<MessageResponseDTO> response = await client.ExecuteAsync<MessageResponseDTO>(request);

            if (response.IsSuccessful)
            {
                string responseData = response.Data.Messages[0].Id;
                return responseData;
            }
            else
            {
                Console.WriteLine("API request failed with status code: " + response.StatusCode);
            }
            return "Error";
        }

        public async Task<bool> UpdateMessageStatus(UpdateMessageDTO updateMessage)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"]);
            string bearerToken = ConfigurationManager.AppSettings["Token"];
            var request = new RestRequest($"/{ConfigurationManager.AppSettings["PhoneNumberID"]}/messages", Method.Post);
            request.AddHeader("Authorization", "Bearer " + bearerToken);

            string requestBody = JsonConvert.SerializeObject(updateMessage);
            request.AddParameter("application/json", requestBody, ParameterType.RequestBody);
            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return true;
            }
            else
            {
                Console.WriteLine("API request failed with status code: " + response.StatusCode);
            }
            return false;
        }
    }
}
