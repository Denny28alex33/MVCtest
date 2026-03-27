using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCtest.DataAccess.Repository.IRepository;
using MVCtest.Models;
using MVCtest.DataAccess.Data;
using MVCtest.Models.ViewModels;

namespace MVCtest.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: CategoryController
        public IActionResult Index()
        {
            List<Product> objCategoryList = _unitOfWork.Product.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVm = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                return View(productVm);
            }
            else
            {
                productVm.Product = _unitOfWork.Product.Get(u => u.ID == id);
                return View(productVm);
            }
            
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVm,IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVm.Product);
                _unitOfWork.save();
                TempData["success"] = "類別新增成功";
                return RedirectToAction("Index");
            }
            else
            {
                productVm.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVm);
            }

            return View();
        }

        // public IActionResult Edit(int? id)
        // {
        //     if (id == null || id == 0)
        //     {
        //         return NotFound();
        //     }
        //
        //     Product? productFromDb = _unitOfWork.Product.Get(u =>u.ID==id);
        //     if (productFromDb == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return View(productFromDb);
        // }
        //
        // [HttpPost]
        // public IActionResult Edit(Product obj)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         _unitOfWork.Product.Update(obj);
        //         _unitOfWork.save();
        //         TempData["success"] = "類別更新成功";
        //         return RedirectToAction("Index");
        //     }
        //
        //     return View();
        // }

        
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product productFromDb = _unitOfWork.Product.Get(u => u.ID==id);
            if (productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? obj = _unitOfWork.Product.Get(u=>u.ID==id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.save();
            TempData["success"] = "類別刪除成功";
            return RedirectToAction("Index");
        }

    }
}
