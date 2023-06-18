using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PostOpinion.Domain;
using PostOpinion.Domain.Entities;
using PostOpinion.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
namespace PostOpinion.Repositories
{
    public class CommentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CommentRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Comment>> GetAllAsync()
        => await _context.Comments.AsNoTracking().ToListAsync();

        public async Task<Comment>? GetByIDAsync(int id)
        => await _context.Comments
                .AsNoTracking()
                .SingleOrDefaultAsync(obj => obj.ID == id);            
        

        public async Task<Comment> CreateAsync(int PostID, CommentForInsertionDTO commentForInsertionDTO)
        {
            //Post post = await _context.Posts.SingleOrDefaultAsync(pst => pst.ID == PostID);
            if ((await _context.Posts.SingleOrDefaultAsync(pst => pst.ID == PostID)) is null)
                throw new InvalidOperationException("There is no such post .");
            commentForInsertionDTO.PostID = PostID;
            Comment comment = _mapper.Map<Comment>(commentForInsertionDTO);
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<bool> UpdateAsync(int ID, CommentForInsertionDTO commentForInsertionDTO)
        {
            var elementUpdater = await _context.Comments.SingleOrDefaultAsync(obj => obj.ID == ID);
            if (elementUpdater is null) throw new InvalidOperationException("There is not such comment .");   
            commentForInsertionDTO.PostID = elementUpdater.PostID;
            _mapper.Map(commentForInsertionDTO, elementUpdater);             
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var elementDeleting = await _context.Comments.SingleOrDefaultAsync(dl => dl.ID == id);
            if (elementDeleting is null)
                throw new InvalidOperationException("There is not such comment .");
            _context.Comments.Remove(elementDeleting);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
