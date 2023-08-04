using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using WhatsAppInMVC.DTOs;
using WhatsAppInMVC.Services;

namespace WhatsAppInMVC.Controllers
{
    public class WhatsAppController : Controller
    {
        private readonly WhatsAppService _whatsAppService;
        private readonly MessageBuilderService _messageBuilderService;

        public WhatsAppController()
        {
            _whatsAppService = new WhatsAppService();
            _messageBuilderService = new MessageBuilderService();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> webhook()
        {
            string hubMode = Request["hub.mode"];
            int hubChallenge = int.Parse(Request["hub.challenge"]);
            string hubVerifyToken = Request["hub.verify_token"];

            int HubChallenge = await _whatsAppService.WebhookVerification(hubMode, hubChallenge, hubVerifyToken);

            if (HubChallenge == -1)
            {
                return HttpNotFound("Not Found");
            }

            return Content(hubChallenge.ToString(), "text/plain");
        }

        [ActionName("webhook")]
        public async Task<ActionResult> ReceiveNotification()
        {
            try
            {
                string jsonData;
                using (var reader = new StreamReader(Request.InputStream))
                {
                    jsonData = reader.ReadToEnd();
                }
                IncomingMessageDTO MessageResponse = await _whatsAppService.ParseMessage(jsonData.ToString());

                var message = _messageBuilderService.BuildTextMessage(MessageResponse.ReceiverPhoneNumber, "Hello this is Umair Mushtaq");
                await _whatsAppService.SendTextMessage(message);
                await _whatsAppService.UpdateMessageStatus(new UpdateMessageDTO()
                {
                    MessagingProduct = "whatsapp",
                    MessageId = MessageResponse.Message.Id,
                    Status = "read"
                });
                return new HttpStatusCodeResult(200, "User Message Send and Read");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(400, ex.Message);
            }
        }
    }
}