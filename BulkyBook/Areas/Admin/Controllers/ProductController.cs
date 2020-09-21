using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public IActionResult Index() //Right-click on Index and then add Index View, or Index.cshtml
        {
            return View();
        }

        //the id is optional (?) because you only need it if you're updating, not when inserting
        public IActionResult Upsert(int? id) //R click on Upsert and add MVC view
        {
            ProductVM productVM = new ProductVM()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if (id == null)
            {
                //this is for create, that's why there's no id
                return View(productVM);
            }

            //this is for edit
            productVM.Product = _unitOfWork.Product.Get(id.GetValueOrDefault());
            if (productVM == null)
            {
                return NotFound();
            }
            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //if cookie val != form val, error raised
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files; //retrieve all files that were uploaded
                if(files.Count > 0) //files was uploaded
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\products");
                    var extension = Path.GetExtension(files[0].FileName);

                    if(productVM.Product.ImageUrl != null)
                    {
                        // this is an edit, so we need to remove old image
                        var imagePath = Path.Combine(webRootPath, productVM.Product.ImageUrl.TrimStart('\\')); //remove the back slashes from the start
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                        using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(filesStreams);
                        }
                        productVM.Product.ImageUrl = @"\images\product" + fileName + extension;
                    }
                    else
                    {
                        //update when they do not change image
                        if(productVM.Product.Id != 0)
                        {
                            Product objFromDb = _unitOfWork.Product.Get(productVM.Product.Id);
                            productVM.Product.ImageUrl = objFromDb.ImageUrl;
                        }
                    }

                    if (productVM.Product.Id == 0)
                    {
                        _unitOfWork.Product.Add(productVM.Product);
                    }
                    else
                    {
                        _unitOfWork.Product.Update(productVM.Product);
                    }
                    return View(productVM);
                }

                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index)); //we don't type in "Index" directly because we want to avoid using magic string
            }
            return View(productVM);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Product.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Product.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });
        }

        #endregion

    }
}
