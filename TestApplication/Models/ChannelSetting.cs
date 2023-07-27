namespace TestApplication.Models
{
    public class ChannelSetting
    {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public required int SendChanId { get; set; }
        public required int TopicId { get; set; }
        public required bool Enabled { get; set; }

        public virtual User? User { get; set; }  
        public virtual SendChan? Channel { get; set; }
        public virtual Topic? Topic { get; set; }    

    } 
}
