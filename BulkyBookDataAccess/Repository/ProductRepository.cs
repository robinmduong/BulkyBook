using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBookDataAccess.Repository; //I couldn't resolve error with Repository<Product> till I added this
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db; //make sure _db and db don't get mixed up

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        //we added this with Ctrl + . because an error popped up when we added Update to IProductRepository.cs but not here
        public void Update(Product product)
        {
            var objFromDb = _db.Products.FirstOrDefault(s => s.Id == product.Id);
            if (objFromDb != null)
            {
                if(product.ImageUrl != null)
                {
                    objFromDb.ImageUrl = product.ImageUrl;
                }
                //this info comes from Product.cs
                objFromDb.ISBN = product.ISBN;
                objFromDb.Price = product.Price;
                objFromDb.Price50 = product.Price50;
                objFromDb.ListPrice = product.ListPrice;
                objFromDb.Price100 = product.Price100;
                objFromDb.Title = product.Title;
                objFromDb.Description = product.Description;
                objFromDb.CategoryId = product.CategoryId;
                objFromDb.Author = product.Author;
                objFromDb.CoverTypeId = product.CoverTypeId;
            }
        }
    }
}
