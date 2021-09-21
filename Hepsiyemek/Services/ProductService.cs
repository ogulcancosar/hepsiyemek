using Hepsiyemek.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hepsiyemek.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(IConfiguration config)
        {

            var client = new MongoClient(config.GetConnectionString("HepsiMongoDb"));
            var database = client.GetDatabase("HepsiDB");
            _products = database.GetCollection<Product>("products");
        }

        public List<Product> Get()
        {
            var test =  _products.Find(new BsonDocument()).ToList();
            return test;
        }

        public Product Get(string id)
        {
            return _products.Find<Product>(Product => Product.id == id).FirstOrDefault();
        }

        public Product Create(Product Product)
        {
            _products.InsertOne(Product);

            return Product;
        }

        public void Update(string id, Product bookIn)
        {
            _products.ReplaceOne(Product => Product.id == id, bookIn);
        }

        public void Remove(Product bookIn)
        {
            _products.DeleteOne(Product => Product.id == bookIn.id);

        }
        public void Remove(string id)
        {
            _products.DeleteOne(Product => Product.id == id);
        }
    }
}
