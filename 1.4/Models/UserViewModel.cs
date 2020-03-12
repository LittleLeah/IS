using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _1._4.Models
{
    public class UserViewModel
    {
        public List<User> users { get; set; }
        public User newUser { get; set; }

        public UserViewModel()
        {
            newUser = new User();
        }
    }
}