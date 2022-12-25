using System;
using System.Data.Entity;

namespace Model
{
    public class Track
    {
        public int TrackID { get; set; }

        public string TrackName { get; set; }

        public string ArtistName { get; set; }
    }

    public class TrackDB : DbContext
    {
        public TrackDB()
        {

        }

        public DbSet<Track> Tracks { get; set; }
    }

}
