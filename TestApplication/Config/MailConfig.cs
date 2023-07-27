namespace TestApplication.Config
{
    public class MailConfig
    {
        public required String FromName { get; set; }
        public required String FromAddress { get; set; }   
        public required String MailServerAddress { get; set; }
        public required int MailServerPort { get; set; }
               
    }
}
