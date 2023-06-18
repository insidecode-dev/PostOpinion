using PostOpinion.Domain.Entities;
using PostOpinion.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostOpinion.Interfaces
{
    public interface IUserService
    {
        public Task<List<User>> GetUsersAsync();

        public Task<User?> GetUserByIDAsync(int id);

        public Task<User> CreateUserAsync(UserDTO userDTO);

        public Task<bool> UpdateUserAsync(int id, UserDTO userDTO);

        public Task<bool> DeleteUserAsync(int id);

        
        
    }
}
