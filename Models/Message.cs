namespace Seazone.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string MessageContent { get; set; }
        public string Status { get; set; } // Default to "Pending"
        public DateTime Date { get; set; } // Automatically populated
    }
}
