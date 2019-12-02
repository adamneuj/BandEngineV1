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

    public class ConversationViewModel
    {
        public Contact Contact { get; set; }
        public Conversation Conversation { get; set; }
        public Email Email { get; set; }
        public List<Conversation> AllConversations { get; set; }
        public string ContactName { get; set; }
    }

    public class ContactViewModel
    {
        public Contact Contact { get; set; }
        public Address Address { get; set; }
        public Email Email { get; set; }
        public string FullAddress { get; set; }
    }

    public class ConcertViewModel
    {
        public Concert Concert { get; set; }
        public Address Address { get; set; }
        public string FullAddress { get; set; }
    }
    public class AllConcertsViewModel
    {
        public List<ConcertViewModel> AllConcerts { get; set; }
    }
}