using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarketplaceV1.Models
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        
        public string SenderId { get; set; }

        public string ReciverId { get; set; }

        public DateTime DateTime { get; set; }

        public string Message { get; set; }

        public bool Seen { get; set; }
    }
}