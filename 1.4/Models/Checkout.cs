using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _1._4.Models
{
    public class Checkout
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime CheckoutDate { get; set; }
        public DateTime CheckinDate { get; set; }

        public Checkout()
        {

        }
    }
}