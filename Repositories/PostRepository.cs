using AutoMapper;
using PostOpinion.Domain.Entities;
using PostOpinion.Domain;
using PostOpinion.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
namespace PostOpinion.Repositories
{
    public class PostRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public PostRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Post>> GetAllAsync()
            => await _context.Posts
               .Include(p => p.Comment)
               .AsNoTracking()
               .ToListAsync();

        public async Task<Post>? GetByIDAsync(int id)
        => await _context.Posts
                .Include(p => p.Comment)
                .AsNoTracking()
                .SingleOrDefaultAsync(obj => obj.ID == id);

        public async Task<Post> CreateAsync(PostForInsertionDTO postForInsertionDTO)
        {
            Post post = _mapper.Map<Post>(postForInsertionDTO);
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();            
            return post;
        }

        public async Task<bool> UpdateAsync(int id, PostForInsertionDTO postForInsertionDTO)
        {
            var elementUpdater = await _context.Posts.Include(cm=>cm.Comment).SingleOrDefaultAsync(obj => obj.ID == id);
            if (elementUpdater is null) throw new InvalidOperationException("There is not such post .");            
            _mapper.Map(postForInsertionDTO, elementUpdater);
            await _context.SaveChangesAsync();
            return true;
        }

        // I was waiting an error when deleting a Post that has comments, but it did not happen . When I delete a post also comments of this Post was deleted . (Interesting)
        public async Task<bool> DeleteAsync(int id)
        {
            var elementDeleter = await _context.Posts.SingleOrDefaultAsync(dl => dl.ID == id);
            if (elementDeleter is null) throw new InvalidOperationException("There is not such post .");
            _context.Posts.Remove(elementDeleter);
            await _context.SaveChangesAsync();
            return true;
        }

        
    }
}

/*
 {
  "text": "birinciUpdatedPost",
  "authorID": 4,
  "publishedDate": "2023-03-11T05:54:12.645Z",
  "commentDTO": [
    {
      "text": "birinciUpdatedComment",
      "authorID": 8
    }
  ]
}




repositories don't accept dto as parameetrs , services accept . We map dto with class and send it to repository 0

 */