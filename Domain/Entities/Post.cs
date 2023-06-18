using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PostOpinion.Domain.Entities
{
    public class Post
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public int AuthorID { get; set; }
        public DateTime PublishedDate { get; set; }        
        public List<Comment> Comment { get; set; }
    }
}
