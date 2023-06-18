
namespace PostOpinion.DTO
{
    public class CommentForInsertionDTO
    {
        public string Text { get; set; }
        public int AuthorID { get; set; }
        //none of JsonIgnore attributes works on FromQuery attribute
        [System.Text.Json.Serialization.JsonIgnore] // this works with FromBody attribute
        //[Newtonsoft.Json.JsonIgnore] //: this  does not work with FromBody attribute
        public int PostID { get; set; }
    }
}
