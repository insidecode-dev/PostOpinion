using PostOpinion.Domain.Entities;
using PostOpinion.DTO;
using PostOpinion.Interfaces;
using PostOpinion.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostOpinion.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;            
        }
        public async Task<User> CreateUserAsync(UserDTO userDTO)
        =>await _userRepository.CreateAsync(userDTO);

        public async Task<bool> DeleteUserAsync(int id)
        =>await _userRepository.DeleteAsync(id);

        public async Task<User> GetUserByIDAsync(int id)
        =>await _userRepository.GetByIDAsync(id);

        public async Task<List<User>> GetUsersAsync()
        =>await _userRepository.GetAllAsync();

        public async Task<bool> UpdateUserAsync(int id, UserDTO userDTO)
        =>await _userRepository.UpdateAsync(id, userDTO);


    }
}
