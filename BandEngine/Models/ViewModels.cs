using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BandEngine.Models
{
    public class ArtistCreationViewModel
    {
        public Artist Artist { get; set; }
        public Address Address { get; set; }

    }

    public class ContactCreationViewModel
    {
        public Contact Contact { get; set; }

        public Address Address { get; set; }

        public Email Email { get; set; }
    }
}