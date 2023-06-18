using PostOpinion.Domain.Entities;
using PostOpinion.DTO;
using PostOpinion.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace PostOpinion.Interfaces
{
    public interface ICommentService
    {
        public Task<List<Comment>> GetCommentsAsync();

        public Task<Comment>? GetCommentByIDAsync(int id);

        public Task<Comment> CreateCommentAsync(int PostID, CommentForInsertionDTO commentForInsertionDTO);

        public Task<bool> UpdateCommentAsync(int id, CommentForInsertionDTO commentForInsertionDTO);

        public Task<bool> DeleteCommentAsync(int id);
        //
        public Task<List<Comment>> CommentsByAuthorIDAsync(int id);
    }
}
