using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Utilities;
using Newtonsoft.Json;
using ServiceStack;

namespace BotNudyk
{
    //[BotAuthentication]
    [AllowAnonymous]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<Message> Post([FromBody]Message message)
        {
            if (message.Type == "Message")
            {
                // calculate something for us to return
                int length = (message.Text ?? string.Empty).Length;

                // return our reply to the user
                return message.CreateReplyMessage($"You sent {length} characters");
            }
            else
            {
                return HandleSystemMessage(message);
            }
        }
        [AllowAnonymous]
        public async Task<bool> Get()
        {
            return true;
        }

        private Message HandleSystemMessage(Message message)
        {
            if (message.Type == "Ping")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Ping: " + message.ToJson();
                return reply;
            }
            else if (message.Type == "DeleteUserData")
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == "BotAddedToConversation")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Bot Added To Conversation: " + message.ToJson();
                return reply;
            }
            else if (message.Type == "BotRemovedFromConversation")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Bot Removed From Conversation: " + message.ToJson();
                return reply;
            }
            else if (message.Type == "UserAddedToConversation")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "User Added To Conversation: " + message.ToJson();
                return reply;
            }
            else if (message.Type == "UserRemovedFromConversation")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "User Removed From Conversation: " + message.ToJson();
                return reply;
            }
            else if (message.Type == "EndOfConversation")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "End Of Conversation: " + message.ToJson();
                return reply;
            }
            else
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Uncnown type'" + message.Type + "': " + message.ToJson();
                return reply;
            }
            return null;
        }
    }
}