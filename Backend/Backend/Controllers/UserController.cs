using Backend.Dto;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Post([FromBody] LoginDto dto)
        {
            var retVal = _userService.Login(dto);
            if (retVal == null) return BadRequest();
            return Ok(retVal);
        }

        [HttpPost("register")]
        public IActionResult Post([FromBody] RegisterDto dto)
        {
            var retVal = _userService.Register(dto);
            if (retVal == null) return BadRequest();
            return Ok();
        }

        [HttpGet("register")]
        public IActionResult GetUser([FromBody] RegisterDto dto)
        {
            var retVal = _userService.Register(dto);
            if (retVal == null) return BadRequest();
            return Ok();
        }

        /*
        [HttpGet]
        [Route("GetUserProfile")]
        //GET : /api/UserProfile
        public async Task<Object> GetUserProfile()
        {
            string userId = _context.Users.Find(id).Orders;
            return new
            {
                user.FullName,
                user.Email,
                user.UserName
            };
        }
        */
    }
}
