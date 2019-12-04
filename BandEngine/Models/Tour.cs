using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BandEngine.Models
{
    public class Tour
    {
        [Key]
        public int TourId { get; set; }

        public string TourName { get; set; }

        [ForeignKey("Concert")]
        public int ConcertId { get; set; }
        public Concert Concert { get; set; }

        [ForeignKey("Artist")]
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
    }
}