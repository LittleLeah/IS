using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _1._4.Models
{
    public class User
    {
        public string Name { get; set; }
        public int UserNo { get; set; }

        public User(string name, int userNo)
        {
            Name = name;
            UserNo = userNo;
        }

        public User()
        {
            Name = "";

        }
    }
}