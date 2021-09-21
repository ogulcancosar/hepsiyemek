using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;

namespace Hepsiyemek.Console
{
    class Program
    {
        public class Product
        {
            public ObjectId id { get; set; }

            public string name { get; set; }
            public string description { get; set; }

           
            public ObjectId categoryId { get; set; }
            public double price { get; set; }
            public string currency { get; set; }
        }

        public class Category
        {           
            public ObjectId id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
        }


        static void Main(string[] args)
        {

            var client = new MongoClient("mongodb+srv://test:1fwNMCqsBLgbkeYD@cluster0.ecz8u.mongodb.net/HepsiDB?retryWrites=true&w=majority");
            var database = client.GetDatabase("HepsiDB");
            var productRepo = database.GetCollection<Product>("products");
            var category = database.GetCollection<Category>("categories");


            Category newCategory = new Category()
            {
                id = new ObjectId(),
                name = "Türk Mutfağı",
                description = "Türk mutfağına ait lezzetler"
            };


            Product newProduct = new Product()
            {
                id = new ObjectId(),
                name = "Döner",
                description = "1 Porsiyon yaprak döner",
                currency="TL",
                price= 25.90,
                categoryId= new ObjectId()
            };

            // Inserting the first document will create collection named "Games"
            //category.InsertOne(newCategory);
            productRepo.InsertOne(newProduct);
        }
    }
}
