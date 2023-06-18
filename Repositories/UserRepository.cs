using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PostOpinion.Domain;
using PostOpinion.Domain.Entities;
using PostOpinion.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace PostOpinion.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UserRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<User>> GetAllAsync()
            => await _context.Users
               .AsNoTracking()
               .ToListAsync();

        public async Task<User>? GetByIDAsync(int id)
        { 
          User user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(obj => obj.ID == id);
            if (user == null) throw new InvalidOperationException("there is not such user");    
            return user;
        } 

        public async Task<User> CreateAsync(UserDTO userDTO)
        {
            User user = _mapper.Map<User>(userDTO);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateAsync(int id, UserDTO userDTO)
        {
            var elementUpdater = await _context.Users.FirstOrDefaultAsync(obj => obj.ID == id);
            if (elementUpdater is null) throw new InvalidOperationException("There is not such post .");
            _mapper.Map(userDTO, elementUpdater);
            await _context.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> DeleteAsync(int id)
        {
            var elementDeleter = await _context.Users.FirstOrDefaultAsync(dl => dl.ID == id);
            if (elementDeleter is null) throw new InvalidOperationException("There is not such post .");
            _context.Users.Remove(elementDeleter);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
