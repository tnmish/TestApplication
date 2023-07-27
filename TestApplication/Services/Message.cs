using TestApplication.Models;

namespace TestApplication.Services
{
    public class Message
    {
        public required string MessageText { get; set; }
        public required Topic Topic { get; set; }
    }
}
