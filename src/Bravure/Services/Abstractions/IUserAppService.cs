using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bravure.Models.User;

namespace Bravure.Services
{
    public interface IUserAppService
    {
        Task<UserDto> CreateAsync(CreateUserDto input);
        Task DeleteAsync(Guid id);
        Task<UserDto> UpdateAsync(UpdateUserDto input);
        Task<UserDto> GetUserAsync(Guid id);
        Task<List<UserDto>> GetAllUsersAsync();
    }
}