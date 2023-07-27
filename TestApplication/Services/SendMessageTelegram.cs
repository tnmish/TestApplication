using TestApplication.Models;
using TestApplication.Database;
using Telegram.Bot;
using Microsoft.Extensions.Options;
using TestApplication.Config;

namespace TestApplication.Services
{
    public class SendMessageTelegram : ISendMessage
    {       
        private readonly IRepository<ChannelSetting> channelset;
        private readonly ILogger<SendMessageMail> _logger;
        private readonly IOptions<TelegramConfig> _tgConfig;

        public SendMessageTelegram(IRepository<ChannelSetting> context, ILogger<SendMessageMail> logger, IOptions<TelegramConfig > tgConfig)
        {
            channelset = context;
            _logger= logger;
            _tgConfig = tgConfig;
        }
        public async Task  SendMessage(User user, Message message) 
        {
            if (user.Phone  == null)
            {
                _logger.LogWarning($"UsrID:{user}, TopicID: {message}: User phone null", user.Id, message.Topic.Id );
            }

            TelegramBotClientOptions tgOptions = new(_tgConfig.Value.Token , _tgConfig.Value.Host , false);
            TelegramBotClient client = new (tgOptions);

            ChannelSetting? cs = await channelset.FindAsync(x => x.Channel.Name == "Telegram"
                                        && x.User.Id == x.UserId
                                        && x.Topic.Id == message.Topic.Id
                                        && x.Enabled);
            if (cs != null)
            {                
                Telegram.Bot.Types.Update[] update = await client.GetUpdatesAsync();
                if (update.Length > 0)
                {
                    foreach (Telegram.Bot.Types.Update upd in update)
                    {
                        if (upd.Message != null)
                        {
                            try 
                            { 
                                await client.SendTextMessageAsync(upd.Message.Chat.Id, message.MessageText);
                                    _logger.LogInformation($"{user} {message}: Send message succeded", user.Id, message.Topic.Id);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, $"UsrID:{user}, TopicID: {message}", user.Id, message.Topic.Id);
                            }
                        }
                    }
                }
            }
            else
            {
                _logger.LogWarning($"UsrID:{user}, TopicID: {message}: Messaging is not allowed", user, message);
            }
                
        }
       
    }

    
}
