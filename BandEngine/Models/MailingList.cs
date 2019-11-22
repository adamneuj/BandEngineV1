using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BandEngine.Models
{
    public class MailingList
    {
        [Key]
        public int MailingListId { get; set; }

        [ForeignKey("Email")]
        public int EmailId { get; set; }
        public virtual Email Email { get; set; }

        [ForeignKey("Artist")]
        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; }
    }
}
