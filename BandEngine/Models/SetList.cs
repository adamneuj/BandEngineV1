using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BandEngine.Models
{
    public class SetList
    {
        [Key]
        public int SetListId { get; set; }
        public int Position { get; set; }

        [ForeignKey("Song")]
        public int SongId { get; set; }
        public virtual Song Song { get; set; }

        [ForeignKey("Concert")]
        public int ConcertId { get; set; }
        public virtual Concert Concert { get; set; }

    }
}
