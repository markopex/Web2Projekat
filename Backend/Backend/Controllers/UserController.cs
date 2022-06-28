using Backend.Dto;
using Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Claims;

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

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult UploadProfilePhoto()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost("apply")]
        [Authorize(Roles = "CUSTOMER")]
        public IActionResult ApplyForDeliverer(string customerUsername)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var username = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            try
            {
                _userService.Apply(customerUsername);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
        [HttpGet("pending-deliverers")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult PendingDelivers()
        {
            return Ok(_userService.PendingUsers());
        }
    }
}
