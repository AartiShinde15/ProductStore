using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductStore.DataAccess.Repositories;
using ProductStore.DataAccess.ViewModels;
using ProductStore.Models;
using System.Diagnostics;

namespace ProductStore.Web.Areas.EndUser.Controllers
{
    [Area("EndUser")]
    public class HomeController : Controller
    {
        private IUnitOfWork _unitofwork;

        public HomeController(IUnitOfWork unitofwork, IWebHostEnvironment hostEnvironment)
        {
            _unitofwork = unitofwork;
        }

        public IActionResult Index()
        {
            ProductVM productVM = new ProductVM();
            productVM.products = _unitofwork.Product.GetAll();
            productVM.categories = convertCategories();
            return View(productVM);
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

        private List<SelectListItem> convertCategories()
        {

            var dummyCategories = new List<SelectListItem>();
            var temp = _unitofwork.Category.GetAll();
            foreach (var c in temp)
            {
                dummyCategories.Add(new SelectListItem
                {
                    Text = c.Name,
                    Value = Convert.ToString(c.Id)
                });
            }
            return dummyCategories;
        }
    }
}