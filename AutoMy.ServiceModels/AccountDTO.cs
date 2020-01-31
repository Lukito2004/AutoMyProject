using AutoMy.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMy.ServiceModels
{
    public class AccountDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<PostDTO> Posts { get; set; } = new List<PostDTO>();
    }
}
