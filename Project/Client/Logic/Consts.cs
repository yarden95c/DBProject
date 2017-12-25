using System;
using System.Collections.Generic;
using BusinessLogic;

namespace Project.Client.Logic
{
    public static class Consts
    {
        public static Dictionary<EntityType, Dictionary<string,string>> FieldsDictionary = new Dictionary<EntityType, Dictionary<string, string>>
        {
            { EntityType.AREA, Area.fields},
            {EntityType.ARTIST, Artist.fields },
            { EntityType.SONG, Song.fields} 
        }; 
        public static class Artist
        {
            public const string ArtistName = "Artist Name:";
            public const string YearOfBirth = "Year Of Birth: ";
            public const string SongOfArtist = "Song Of This Artist: ";
            public const string LivingPlace = "Living Place: ";
            public static Dictionary<string, string> fields = new Dictionary<string, string>
            {
                {ArtistName,"name"},
                {YearOfBirth,"begin_date_year" },
                {SongOfArtist, "list_of_songs" },
                {LivingPlace, "area_Id"}

            };
        };
        public static class Song
        {
            public const string SongName = "Song Name: ";
            public const string WhoSingIt = "Who Sing It: ";
            public const string Year = "Year: ";
            public static Dictionary<string, string> fields = new Dictionary<string, string>
            {
                {SongName,"song_name"},
                {WhoSingIt, "artist_name"},
                {Year,"release_date_year" }
            };
        }

        public static class Area
        {
            public const string PlaceName = "Place Name:";
            public const string ArtistWhoLivedThere = "Artist Who Lived There: ";
            public const string SongWrittenThere = "Song Written There: ";
            public static Dictionary<string, string> fields = new Dictionary<string, string>
            {
                {PlaceName,"name_area"},
                {ArtistWhoLivedThere, "list_of_artists"},
                {SongWrittenThere, "list_of_artists" }
            };
        }
    }
}
