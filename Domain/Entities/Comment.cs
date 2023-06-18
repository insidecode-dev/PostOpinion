using System.Text.Json.Serialization;

namespace PostOpinion.Domain.Entities
{
    public class Comment
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public int AuthorID { get; set; }
        public int PostID { get; set; }
        [JsonIgnore]
        public Post Post { get; set; }
    }
}
