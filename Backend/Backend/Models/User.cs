using System.Collections.Generic;

namespace Backend.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthday { get; set; }
        public string Address { get; set; }
        public EUserType Type { get; set; }
        public string Picture { get; set; }
        public List<Order> Orders { get; set; }
        public List<Order> OrdersToDiliver { get; set; }
    }
}
