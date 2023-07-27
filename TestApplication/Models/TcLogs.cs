namespace TestApplication.Models
{
    public class TcLogs
    {
        public int Id { get; set; }
        public required string Level{ get; set; }
        public required string Message { get; set; }
        public required DateTime LogDate { get; set; }
        public required string Exception { get; set; }
        public string? CallSite { get; set; }
        public required string  Trace { get; set; }
        public required string Logger { get; set; }
        
    }
}
