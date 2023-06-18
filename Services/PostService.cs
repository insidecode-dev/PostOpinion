using PostOpinion.Domain.Entities;
using PostOpinion.DTO;
using PostOpinion.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using PostOpinion.Interfaces;

namespace PostOpinion.Services
{
    public class PostService:IPostService
    {
        private readonly PostRepository _postRepository;
        public PostService(PostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<List<Post>> GetPostsAsync()
            => await _postRepository.GetAllAsync();

        public async Task<Post?> GetPostByIDAsync(int id)
        {
            var element = await _postRepository.GetByIDAsync(id);
            if (element is not null) return element;
            throw new InvalidOperationException("There is not such post .");
        }

        public async Task<Post> CreatePostAsync(PostForInsertionDTO postForInsertionDTO)
        => await _postRepository.CreateAsync(postForInsertionDTO);

        public async Task<bool> UpdatePostAsync(int id, PostForInsertionDTO postForInsertionDTO)
        => await _postRepository.UpdateAsync(id, postForInsertionDTO);

        public async Task<bool> DeletePostAsync(int id)
        => await _postRepository.DeleteAsync(id);

        //
        public async Task<List<Comment>> CommentByPostIDAsync(int id)
        {
            var post = await _postRepository.GetByIDAsync(id);
            if (post == null) throw new InvalidOperationException("There is not such post .");
            var comments = from cm in post.Comment select cm;
            return comments.ToList();
        }

        
    }
}
