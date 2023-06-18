using PostOpinion.Domain.Entities;
using PostOpinion.DTO;
using PostOpinion.Interfaces;
using PostOpinion.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostOpinion.Services
{
    public class CommentService : ICommentService
    {
        private readonly CommentRepository _commentRepository;
        public CommentService(CommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }



        public async Task<List<Comment>> GetCommentsAsync()
            => await _commentRepository.GetAllAsync();

        public async Task<Comment>? GetCommentByIDAsync(int id)
        {
            var element = await _commentRepository.GetByIDAsync(id);
            if (element is null) throw new InvalidOperationException("There is not such comment .");
            return element;
        }

        public async Task<Comment> CreateCommentAsync(int PostID, CommentForInsertionDTO commentForInsertionDTO)
        => await _commentRepository.CreateAsync(PostID, commentForInsertionDTO);

        public async Task<bool> UpdateCommentAsync(int id, CommentForInsertionDTO commentForInsertionDTO)
        => await _commentRepository.UpdateAsync(id, commentForInsertionDTO);

        public async Task<bool> DeleteCommentAsync(int id)
        => await _commentRepository.DeleteAsync(id);
        //
        public async Task<List<Comment>> CommentsByAuthorIDAsync(int id)
        {
            var comments = await _commentRepository.GetAllAsync();
            var assosiatedComments = (from auth in comments where auth.AuthorID == id select auth).ToList();
            if (assosiatedComments.Count == 0) throw new InvalidOperationException("There is not a comment published by this Author .");
            return assosiatedComments;
        }
    }
}
