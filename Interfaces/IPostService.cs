using PostOpinion.Domain.Entities;
using PostOpinion.DTO;
using PostOpinion.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace PostOpinion.Interfaces
{
    public interface IPostService
    {
        public Task<List<Post>> GetPostsAsync();

        public Task<Post?> GetPostByIDAsync(int id);

        public Task<Post> CreatePostAsync(PostForInsertionDTO postForInsertionDTO);

        public Task<bool> UpdatePostAsync(int id, PostForInsertionDTO postForInsertionDTO);

        public Task<bool> DeletePostAsync(int id);

        //
        public Task<List<Comment>> CommentByPostIDAsync(int id);
    }
}
