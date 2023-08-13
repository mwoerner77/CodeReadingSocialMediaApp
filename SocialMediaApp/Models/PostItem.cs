namespace SocialMediaApp.Models
{
    //20:
    //
    public class PostItem
    {
        public long Id { get; set; }
        public string? Username { get; set; }
        public string? Text { get; set; }
        public int Likes { get; set; }
        public int Views { get; set; }
        public DateTime? CreationTime { get; set; }
    }
}
