using Backend.Dto;

namespace Backend.Interfaces
{
    public interface IUserService
    {
        string Login(LoginDto loginDto);
        UserDto Register(RegisterDto registerDto);
    }
}
