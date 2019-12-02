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

    public class ArtistTaskViewModel
    {
        public List<ArtistTask> ArtistTask { get; set; }
        public List<ArtistTask> InProgress { get; set; }
        public List<ArtistTask> Done { get; set; }

    }
}