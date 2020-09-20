using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Areas.Admin.Controllers //after this, we need to work on js
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        //add this newline of code--we need IWebHostEnvironment because we'll be uploading images on the server in a folder inside
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        //the id is optional (?) because you only need it if you're updating, not when inserting
        public IActionResult Upsert(int? id) //R click on Upsert and add MVC view
        {
            Product product = new Product();

            if (id == null)
            {
                //this is for create, that's why there's no id
                return View(product);
            }

            //this is for edit
            product = _unitOfWork.Product.Get(id.GetValueOrDefault());
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //if cookie val != form val, error raised
        public IActionResult Upsert(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.Id == 0)
                {
                    _unitOfWork.Product.Add(product);
                }
                else
                {
                    _unitOfWork.Product.Update(product);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index)); //we don't type in "Index" directly because we want to avoid using magic string
            }
            return View(product);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Category.GetAll();
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Category.Get(id);
            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Category.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });
        }

        #endregion

    }
}
