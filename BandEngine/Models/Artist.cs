using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BandEngine.Models
{
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

        public string AssociatedAct { get; set; }
        public string Genre { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
