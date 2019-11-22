using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BandEngine.Models
{
    public class Email
    {
        [Key]
        public int EmailId { get; set; }
        public string EmailAddress { get; set; }
    }
}
