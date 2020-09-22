using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork //inplement IUnit of Work and will implement IUnitOfWork's interfaces
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db); //this way, when you have unit of work, you'll be able to access the category repository
            CoverType = new CoverTypeRepository(_db); //Step 6. - Configure UnitOfWork
            Product = new ProductRepository(_db);

            //Don't forget to go to Tools > NuGet Pkg Manager > Console > 
            //Set it to Default project: BulkyBook.DataAccess first. Type in add-migration addCoverTypeToDb
            //once successful, type update-database and check SQL Server for successful update. dbo.CoverType is a new table under BulkyBook

            SP_Call = new SP_Call(_db);
        }

        public ICategoryRepository Category { get; private set; }
        public ICoverTypeRepository CoverType { get; private set; }
        public IProductRepository Product { get; private set; }

        //ISP = I (interface) stored procedure)
        public ISP_Call SP_Call { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        //save method - needed because inside Repositoryc.cs, we have Remove methods, but we are not saving the changes anywhere in our repo
        //similarly, there is no save inside of CategoryRepository.cs
        //all changes will be saved when you call this Save method inside Unit of Work, which is at the parent level:
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
