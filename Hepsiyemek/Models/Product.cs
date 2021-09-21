using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hepsiyemek.Models
{
    public class Product
    {
        [BsonId]

        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        public string name { get; set; }
        public string description { get; set; }


        //[BsonId]

        [BsonRepresentation(BsonType.ObjectId)]
        public string categoryId { get; set; }
        public double price { get; set; }
        public string currency { get; set; }

        [BsonIgnore]
        public Category category { get; set; }
    }
}
