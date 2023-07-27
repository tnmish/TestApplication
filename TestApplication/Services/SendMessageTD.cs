using TestApplication.Models;
using TestApplication.Database;
using TdLib;
using Microsoft.Extensions.Options;
using TestApplication.Config;


namespace TestApplication.Services
{
    public class SendMessageTD : ISendMessage
    {       
        private readonly IRepository<ChannelSetting> channelset;
        private readonly ILogger<SendMessageMail> _logger;
        private readonly IOptions<TelegramConfig> _tgConfig;
                        
        private const int ApiId = 0;
        private const string ApiHash = "sample_hash";

        private const string PhoneNumber = "Номер телефона";
        private const string ApplicationVersion = "1.0.1";

        private static TdClient  _client = new();
              

        public SendMessageTD(IRepository<ChannelSetting> context, ILogger<SendMessageMail> logger, IOptions<TelegramConfig > tgConfig)
        {
            channelset = context;
            _logger= logger;
            _tgConfig = tgConfig;
            _client = new TdClient();
        }
        public async Task  SendMessage(User user, Message message) 
        {
            if (user.Phone  == null)
            {
                _logger.LogWarning($"UsrID:{user}, TopicID: {message}: User phone null", user.Id, message.Topic.Id );
            }          

            ChannelSetting? cs = await channelset.FindAsync(x => x.Channel.Name == "Telegram"
                                        && x.User.Id == x.UserId
                                        && x.Topic.Id == message.Topic.Id
                                        && x.Enabled);
            
            if (cs != null)
            {
                _client = new TdClient();
                TdApi.JsonValue val;
                await _client.AddProxyAsync("localhost", 9000, true, null);
                //_client.status
                
                val = await _client.GetApplicationConfigAsync();
                
                await _client.CheckAuthenticationCodeAsync("sample_hash");
                TdApi.InputMessageContent msg = new ();                

                await TdApi.SendMessageAsync(_client, 0, 0, 0, null, null, msg);
               
            }
            else
            {
                _logger.LogWarning($"UsrID:{user}, TopicID: {message}: Messaging is not allowed", user, message);
            }
                
        }
        

    }

    
}
