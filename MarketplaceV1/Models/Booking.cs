using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarketplaceV1.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string BloggerNickname { get; set; }
    }
}