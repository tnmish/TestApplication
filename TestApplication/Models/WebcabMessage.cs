namespace TestApplication.Models
{
    public class WebcabMessage
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TopicId { get; set; }
        
        public required string Message { get; set; } 
        public DateTime DateTime { get; set; }
        public virtual Topic? Topic { get; set; }
        public virtual User? User { get; set; }

    }
}
