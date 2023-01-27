using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityByExamples.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

       
        public string RENT { get; set; }

      
        public string ADDRESS { get; set; }

       
       
        public int ROOM { get; set; }
        public int BATHROOM { get; set; }
        public IFormFile Image { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }
    }
}
