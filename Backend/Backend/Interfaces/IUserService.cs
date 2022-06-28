using Backend.Dto;
using System.Collections.Generic;

namespace Backend.Interfaces
{
    public interface IUserService
    {
        string Login(LoginDto loginDto);
        UserDto Register(RegisterDto registerDto);
        void Apply(string customerUsername);
        public List<UserDto> PendingUsers();
    }
}
