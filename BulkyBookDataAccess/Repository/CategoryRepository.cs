using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBookDataAccess.Repository; //I couldn't resolve error with Repository<Category> till I added this
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db; //make sure _db and db don't get mixed up

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        //we added this with Ctrl + . because an error popped up when we added Update to ICategoryRepository.cs but not here
        public void Update(Category category)
        {
            var objFromDb = _db.Categories.FirstOrDefault(s => s.Id == category.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = category.Name;
                _db.SaveChanges();
            }
        }
    }
}
