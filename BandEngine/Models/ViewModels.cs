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
    public class SetListViewModel
    {
        public List<SetList> SetList { get; set; }
        public List<Song> AllSongs { get; set; }
        public Concert CurrentConcert { get; set; }
    }
    public class AlbumSongsViewModel
    {
        public List<Song> AllSongs { get; set; }
        public List<Song> SongsOnAlbum { get; set; }
        public Album CurrentAlbum { get; set; }
    }

    public class TourViewModel
    {
        public List<Concert> AllConcerts { get; set; }
        public List<Concert> AllStops { get; set; }
        public Tour CurrentTour { get; set; }
    }
}