using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BandEngine.Models
{
    public class ArtistCreation
    {
        public Artist Artist { get; set; }
        public Address Address { get; set; }

    }

    public class ContactCreation
    {
        public Contact Contact { get; set; }

        public Address Address { get; set; }

        public Email Email { get; set; }
    }
}