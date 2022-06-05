using Backend.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private RestorauntDbContext _context;
        public UserController(RestorauntDbContext context)
        {
            _context = context;
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
