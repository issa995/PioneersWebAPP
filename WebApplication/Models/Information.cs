using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Information
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Address { get; set; }
        public string TaxNumber { get; set; }
        [Required]
        public bool Active { get; set; }
        public string Logo { get; set; }
    }
}