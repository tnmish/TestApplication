using TestApplication.Models;
using TestApplication.Database;

namespace TestApplication.Services
{
    public class SendMessageWebcab : ISendMessage
    {
        private readonly IRepository<ChannelSetting> channelset;
        private readonly IRepository<WebcabMessage> webcabset;
        private readonly IRepository<Topic> topicset;
        private readonly ILogger<SendMessageMail> _logger;
        
        public SendMessageWebcab(IRepository<ChannelSetting> context, IRepository<WebcabMessage> wcontext, IRepository<Topic> tcontext, ILogger<SendMessageMail> logger)
        {
            channelset = context;
            webcabset = wcontext;
            topicset = tcontext;
            _logger = logger;
        }

        public async Task SendMessage(User user, Message message) 
        {
            if (user == null)
            {
                _logger.LogWarning($"UserID: {user} TopicID: {message}: User mail is null", user.Id, message.Topic.Id);
            }
            
            Topic? topic = await topicset.FindByIdAsync(message.Topic.Id);  //db.Topics.FirstOrDefaultAsync(x => x.Id == message.Topic.Id );
            if (topic == null)
            {
                _logger.LogWarning($"UserID: {user} TopicID: {message}: Topic is null", user.Id, message.Topic.Id);
            }

            ChannelSetting? cs = await channelset.FindAsync(x => x.Channel.Name == "Webcab"
                                        && x.User.Id == x.UserId
                                        && x.Topic.Id == message.Topic.Id
                                        && x.Enabled);
            if (cs != null)
            {
                WebcabMessage msg = new()
                {
                    Topic = topic,
                    Message = message.MessageText,
                    User = user,
                    DateTime = DateTime.Now
                };
                try
                {
                    await webcabset.CreateAsync(msg);
                    _logger.LogInformation($"UserID: {user} TopicID: {message}: Send message succeded", user.Id, message.Topic.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"UserID: {user} TopicID: {message}:", user.Id, message.Topic.Id);
                }
               
            }
            else
            {
                _logger.LogWarning($"UserID: {user} TopicID: {message}: Messaging is not allowed", user.Id, message.Topic.Id);
            }

            
        }
    }
}
