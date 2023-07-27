using TestApplication.Models;

namespace TestApplication.Services
{
    public interface ISendMessage 
    {
        public Task SendMessage (User user, Message message);
    }
}
