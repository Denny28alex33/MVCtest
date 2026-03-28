using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCtest.Models;
// 🌟 漏網之魚：課本沒說，但你必須加上這行，不然 IUnitOfWork 會報錯 (紅字)！
using MVCtest.DataAccess.Repository.IRepository; 

namespace MVCtest.Areas.Customer.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork; // 👈 宣告大總管變數

    // 👈 建立建構式 (Constructor)：告訴系統啟動時要給我這兩個工具
    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    // 👈 修改首頁的動作
    public IActionResult Index()
    {
        // 叫大總管去資料庫撈所有的產品，並且順便把「Category (類別)」的資料也帶出來
        IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
        
        // 把撈出來的一大串產品資料，塞進 View 裡面傳給前端網頁
        return View(productList);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}