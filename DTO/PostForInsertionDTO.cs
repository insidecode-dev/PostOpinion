using System.Collections.Generic;
using System;

namespace PostOpinion.DTO
{
    public class PostForInsertionDTO
    {
        public string Text { get; set; }
        public int AuthorID { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
