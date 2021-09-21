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
    public class CategoryService
    {
        private readonly IMongoCollection<Category> _categories;



        public CategoryService(IConfiguration config)
        {

            var client = new MongoClient(config.GetConnectionString("HepsiMongoDb"));
            var database = client.GetDatabase("HepsiDB");
            _categories = database.GetCollection<Category>("categories");
        }

        public List<Category> Get()
        {
            return _categories.Find(new BsonDocument()).ToList();
        }

        public Category Get(string id)
        {
            return _categories.Find<Category>(category => category.id == id).FirstOrDefault();
        }


        public Category Create(Category category)
        {
            _categories.InsertOne(category);
            return category;
        }

        public void Update(string id, Category bookIn)
        {
            _categories.ReplaceOne(category => category.id == id, bookIn);
        }

        public void Remove(Category bookIn)
        {
            _categories.DeleteOne(category => category.id == bookIn.id);
        }

        public void Remove(string id)
        {
            _categories.DeleteOne(category => category.id == id);
        }
    }
}
