﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotNetBooksAPI.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
    }
}




