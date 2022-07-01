﻿using Backend.Models;
using System.Collections.Generic;

namespace Backend.Dto
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long Birthday { get; set; }
        public string Address { get; set; }
        public EUserType Type { get; set; }
        public string Picture { get; set; }
        public ERequestStatus DelivererRequestStatus { set; get; }
        public List<Order> Orders { get; set; }
        public List<Order> OrdersToDiliver { get; set; }
    }
}
