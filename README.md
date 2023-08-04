# WhatsApp Business API For Chatbot

This is just for tutorial purpose. Developed using ASP .NET MVC v4.7.

Welcome to the [WhatsApp Business API For Chatbot] repository! This project is dedicated to WhatsApp's Business API integration with ASP .NET MVC (v4.7). The project includes some basic functionality e.g, setting up Webhook, receiving changes on our webhook endpoint and sending message to a number.

Getting Started
Follow these simple steps to get the project up and running on your local machine.

(1) You need ngrok with a domain (it's free) and hook the project with ngrok domain.

I used this command ngrok http --domain=your-ngrok-domain https://localhost:44394 --host-header=localhost:44394

(2) Add these values in web.config

key="BaseUrl" value="https://graph.facebook.com/v17.0"

key="Token" value="API_Token"

key="PhoneNumberID" value="From_Meta_API_Setup"

key="WebHookVerificationToken" value="Can be set anything. Will be used when verifying the webhook on Meta API Setup"
