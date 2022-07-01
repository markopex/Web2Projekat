using Backend.Dto;
using System.Collections.Generic;

namespace Backend.Interfaces
{
    public interface IUserService
    {
        UserDto GetUser(string username);
        UserDto UpdateUser(UserDto user);
        TokenDto Login(LoginDto loginDto);
        UserDto Register(RegisterDto registerDto);
        void Apply(string customerUsername);
        public List<UserDto> PendingUsers();
    }
}
