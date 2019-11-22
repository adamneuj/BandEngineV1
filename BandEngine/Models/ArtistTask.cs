using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BandEngine.Models
{
    public class ArtistTask
    {
        [Key]
        public int TaskId { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Progress { get; set; }

        [ForeignKey("Artist")]
        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; }
    }
}
