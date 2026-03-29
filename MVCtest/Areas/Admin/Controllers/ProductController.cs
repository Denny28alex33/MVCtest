using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCtest.DataAccess.Repository.IRepository;
using MVCtest.Models;
using MVCtest.DataAccess.Data;
using MVCtest.Models.ViewModels;
using MVCtest.Utility;

namespace MVCtest.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(SD.Role_Admin)]
    public class ProductController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: CategoryController
        public IActionResult Index()
        {
            List<Product> objCategoryList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
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
        public IActionResult Upsert(ProductVM productVm, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            
                    // 🌟 修正 1：Mac 適用的路徑寫法 (使用正斜線 / )
                    string productPath = Path.Combine(wwwRootPath, @"images/product");

                    if (!string.IsNullOrEmpty(productVm.Product.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, productVm.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
            
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    // 🌟 修正 2：網頁 URL 也要用正斜線 /
                    productVm.Product.ImageUrl = @"/images/product/" + fileName; 
                }

                if (productVm.Product.ID == 0)
                {
                    _unitOfWork.Product.Add(productVm.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productVm.Product);
                }
                _unitOfWork.save();
                TempData["success"] = "產品新增成功！";
                return RedirectToAction("Index");

                // 🌟 修正 3：補上 Upsert 的判斷靈魂！(有 ID 是更新，沒 ID 是新增)
                if (productVm.Product.ID == 0)
                {
                    _unitOfWork.Product.Add(productVm.Product);
                    TempData["success"] = "產品新增成功"; // 順便幫你改掉錯字
                }
                else
                {
                    _unitOfWork.Product.Update(productVm.Product);
                    TempData["success"] = "產品更新成功";
                }
        
                _unitOfWork.save();
                return RedirectToAction("Index");
            }
            else
            {
                // 萬一驗證失敗，要把下拉選單重新準備好送回畫面上
                productVm.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVm);
            }
            // 🌟 修正 4：移除了最底下那個永遠執行不到的 return View();
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

        
        // public IActionResult Delete(int? id)
        // {
        //     if (id == null || id == 0)
        //     {
        //         return NotFound();
        //     }
        //
        //     Product productFromDb = _unitOfWork.Product.Get(u => u.ID==id);
        //     if (productFromDb == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return View(productFromDb);
        // }
        //
        // [HttpPost, ActionName("Delete")]
        // public IActionResult DeletePOST(int? id)
        // {
        //     Product? obj = _unitOfWork.Product.Get(u=>u.ID==id);
        //     if (obj == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _unitOfWork.Product.Remove(obj);
        //     _unitOfWork.save();
        //     TempData["success"] = "類別刪除成功";
        //     return RedirectToAction("Index");
        // }

        #region API CALLS

        [HttpGet]
        public IActionResult GetALL()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList }
        );
    }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.ID == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "刪除失敗" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath,
                productToBeDeleted.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.save();
            return Json(new { success = true, message = "新增成功" });
        }
        #endregion

    }
}
