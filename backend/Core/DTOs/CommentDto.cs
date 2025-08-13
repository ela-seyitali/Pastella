namespace Pastella.Backend.Core.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int CakeId { get; set; }
    }
}