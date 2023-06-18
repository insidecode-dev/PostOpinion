using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using PostOpinion.Domain.Entities;
using PostOpinion.DTO;
using PostOpinion.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostOpinion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("All Comments")]
        public async Task<List<Comment>> GetAllCommentsAsync() => await _commentService.GetCommentsAsync();


        [HttpGet("{ID}/Comment")]
        public async Task<Comment> GetCommentAsync([FromRoute] int ID) => await _commentService.GetCommentByIDAsync(ID);

        [HttpPost("{PostID}/Create Comment")]
        public async Task<Comment> CreateCommentAsync([FromRoute] int PostID, [FromBody] CommentForInsertionDTO commentForInsertionDTO) => await _commentService.CreateCommentAsync(PostID, commentForInsertionDTO);

        [HttpPut("{ID}/Update Comment")]
        public async Task<IActionResult> UpdateCommentAsync([FromRoute] int ID, [FromBody] CommentForInsertionDTO commentForInsertionDTO)
        {
            var value = await _commentService.UpdateCommentAsync(ID, commentForInsertionDTO);
            if (!value) return BadRequest();
            return Ok();
        }

        [HttpDelete("{ID}/Delete Comment")]
        public async Task<IActionResult> DeleteCommentAsync([FromRoute] int ID) {
            var value = await _commentService.DeleteCommentAsync(ID);
            if (!value) return BadRequest();
            return Ok();
        } 

        [HttpGet("{ID}/Comment by AuthorID")]
        public async Task<List<Comment>> GetCommentsByAuthorIDAsync([FromRoute]int ID)=> await _commentService.CommentsByAuthorIDAsync(ID);   
    }
}
