using Microsoft.Extensions.Options;
using System.Net.Mail;
using TestApplication.Config;
using TestApplication.Database;
using TestApplication.Models;

namespace TestApplication.Services
{
    public class SendMessageMail : ISendMessage
    {
        private readonly IRepository<ChannelSetting> channelset;
        private readonly ILogger<SendMessageMail> _logger;
        private readonly IOptions<MailConfig> mailCfg;

        public SendMessageMail(IRepository<ChannelSetting> context, ILogger <SendMessageMail> logger, IOptions<MailConfig> mailConfig)
        {
            channelset = context;
            _logger = logger;
            mailCfg = mailConfig;
        }

        public async Task SendMessage(User user, Message message) 
        {
            if (user.Email == null)
            {
                _logger.LogWarning($"UserID: {user} TopicID: {message}: User mail is null", user.Id, message.Topic.Id);
                
            }

            ChannelSetting? cs = await channelset.FindAsync(x => x.Channel.Name == "Email"
                                        && x.User.Id == x.UserId
                                        && x.Topic.Id == message.Topic.Id
                                        && x.Enabled);
            if (cs != null)
            { 
                MailAddress from = new (mailCfg.Value.FromAddress, mailCfg.Value.FromName);
                MailAddress to = new(user.Email, user.Name);
                MailMessage m = new(from, to)
                {
                    Subject = message.Topic.Name,
                    Body = message.MessageText
                };

                //SmtpClient smtp = new("localhost", 1025);
                SmtpClient smtp = new(mailCfg.Value.MailServerAddress, mailCfg.Value.MailServerPort);
                try
                {
                    await smtp.SendMailAsync(m);
                    _logger.LogInformation($"UserID: {user} TopicID: {message}: Send message succeded", user.Id, message.Topic.Id);
             
                }
                catch (Exception ex)
                {
                    _logger.LogError (ex, $"UserID: {user} TopicID: {message}:", user.Id, message.Topic.Id );                 
                }
                
                
            }
            else
            {
                _logger.LogWarning($"UserID: {user} TopicID: {message}: Messaging is not allowed", user.Id, message.Topic.Id);
               
            }
        }
    }
}
