using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
//using Newtonsoft.Json.Linq;
using PostOpinion.Domain.Entities;
using PostOpinion.DTO;
using PostOpinion.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PostOpinion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IConfiguration _configuration;
        public PostController(IPostService postService, IConfiguration configuration)
        {
            _postService = postService;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("Posts")]
        public async Task<List<Post>> GetAllPosts() =>
            await _postService.GetPostsAsync();

        [HttpGet("{ID}/Post")]
        public async Task<Post> GetPostByIDAsync([FromRoute] int ID) =>
            await _postService.GetPostByIDAsync(ID);

        [HttpPost("Create Post")]
        public async Task<Post> CreatePostAsync([FromQuery] PostForInsertionDTO postForInsertionDTO) =>
            await _postService.CreatePostAsync(postForInsertionDTO);

        [HttpPut("{ID}/Update Post")]
        public async Task<IActionResult> UpdatePostAsync([FromRoute] int ID, [FromBody] PostForInsertionDTO postForInsertionDTO)
        {
            var value = await _postService.UpdatePostAsync(ID, postForInsertionDTO);
            if (!value) return BadRequest();
            return Ok();
        }

        
        [HttpDelete("{ID}/Delete Post")]
        public async Task<IActionResult> DeletePostAsync([FromRoute] int ID)
        {
            var value = await _postService.DeletePostAsync(ID);
            if (!value) return BadRequest();
            return Ok();
        }

        [HttpGet("{ID}/Comments By PostID")]
        public async Task<List<Comment>> GetCommentsByPostIDAsync(int ID) => await _postService.CommentByPostIDAsync(ID);

        /////
        ///
        [HttpGet("LogIn")]
        public async Task<string> Login([FromQuery] LogInDTO logInDTO)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]); //
            var tokenDescriptor = new SecurityTokenDescriptor 
            {
                Subject = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.Name, logInDTO.Name),
                new Claim(ClaimTypes.Surname, logInDTO.SurName),
                new Claim(ClaimTypes.Email, logInDTO.Email),
                })
,
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
