using AutoMapper;
using Backend.Dto;
using Backend.Infrastructure;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Backend.Service
{
    public class UserService: IUserService
    {
        private readonly IMapper _mapper;
        private readonly RestorauntDbContext _dbContext;

        private readonly IConfigurationSection _secretKey;

        public UserService(IConfiguration config, IMapper mapper, RestorauntDbContext dbContext)
        {
            _secretKey = config.GetSection("SecretKey");
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public void Apply(string customerUsername)
        {
            User user = _dbContext.Users.ToList().Find(x => x.Username == customerUsername.ToLower());
            if (user != null) throw new Exception("There is no user with username: " + customerUsername);

            if (user.Type != EUserType.CUSTOMER) throw new Exception("User is not customer!");
            if (user.DelivererRequestStatus == ERequestStatus.NO_REQUEST) throw new Exception("Customer already applied!");

            user.DelivererRequestStatus = ERequestStatus.PEDNING;
            _dbContext.SaveChanges();
        }


        public string Login(LoginDto loginDto)
        {
            User user = _dbContext.Users.ToList().Find(x => x.Username == loginDto.Username);

            if (user == null)
                return null;

            if (BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))//Uporedjujemo hes pasvorda iz baze i unetog pasvorda
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role, UserType.GetString(user.Type))); //Add user type to claim
                claims.Add(new Claim(ClaimTypes.Name, user.Username));
                //Kreiramo kredencijale za potpisivanje tokena. Token mora biti potpisan privatnim kljucem
                //kako bi se sprecile njegove neovlascene izmene
                SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:44345", //url servera koji je izdao token
                    claims: claims, //claimovi
                    expires: DateTime.Now.AddMinutes(20), //vazenje tokena u minutama
                    signingCredentials: signinCredentials //kredencijali za potpis
                );
                string tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return tokenString;
            }
            else
            {
                return null;
            }
        }

        public List<UserDto> PendingUsers()
        {
            return _mapper.Map<List<UserDto>>(_dbContext.Users.ToList().FindAll(i => i.DelivererRequestStatus != ERequestStatus.NO_REQUEST));
        }

        public UserDto Register(RegisterDto registerDto)
        {
            User user = _dbContext.Users.ToList().Find(x => x.Username == registerDto.Username);
            if (user != null) return null;

            user = _mapper.Map<User>(registerDto);
            user.Type = EUserType.CUSTOMER;
            user.Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            user.Username = user.Username.ToLower();

            _dbContext.Add(user);
            _dbContext.SaveChanges();

            var retVal = _mapper.Map<UserDto>(_dbContext.Users.ToList().Find(x => x.Username == registerDto.Username));

            return retVal;
        }
    }
}
