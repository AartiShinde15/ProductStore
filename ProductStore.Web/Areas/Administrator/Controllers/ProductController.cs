using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductStore.DataAccess.Repositories;
using ProductStore.DataAccess.ViewModels;
using ProductStore.Models;

namespace ProductStore.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class ProductController : Controller
    {
        private IUnitOfWork _unitofwork;
        private IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork unitofwork, IWebHostEnvironment hostEnvironment)
        {
            _unitofwork = unitofwork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            ProductVM productVM = new ProductVM();
            productVM.products = _unitofwork.Product.GetAll();
            productVM.categories = convertCategories();
            return View(productVM);
        }

        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            ProductVM productVM = new ProductVM();
            productVM.categories = convertCategories();
            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.product = _unitofwork.Product.GetT(x => x.Id == id);
                TempData["ImageUrl"] = productVM.product.ImageUrl;
                TempData.Keep();
                if (productVM.product == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(productVM);
                }
            }
        }

        private ProductVM getImageFile(ProductVM productVM, IFormFile? file)
        {
            string fileName = string.Empty;
            if (file != null)
            {
                string uploadDir = Path.Combine(_hostEnvironment.WebRootPath, "ProductImage");
                fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                if (productVM.product.ImageUrl != null)
                {
                    var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, productVM.product.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                productVM.product.ImageUrl = @"\ProductImage\" + fileName;
            }
            return productVM;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(ProductVM productVM, IFormFile? file)
        {
            productVM = getImageFile(productVM, file);
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    productVM.categories = convertCategories();
                    if (TempData["ImageUrl"] != null)
                    {
                        productVM.product.ImageUrl = TempData["ImageUrl"].ToString();
                        TempData["ImageUrl"] = "";
                    }
                    else
                    {
                        ModelState.AddModelError("product.ImageUrl", "Please select file.");
                        return View(productVM);
                    }
                }
                if (productVM.product.Id == 0)
                {
                    _unitofwork.Product.Add(productVM.product);
                    TempData["success"] = "Product created successfully!!!";
                }
                else
                {
                    _unitofwork.Product.Update(productVM.product);
                    TempData["success"] = "Product updated successfully!!!";
                }
                _unitofwork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                productVM.categories = convertCategories();
                return View(productVM);
            }
        }

        private List<SelectListItem> convertCategories()
        {

            var tempCategories = new List<SelectListItem>();
            var temp = _unitofwork.Category.GetAll();
            foreach (var c in temp)
            {
                tempCategories.Add(new SelectListItem
                {
                    Text = c.Name,
                    Value = Convert.ToString(c.Id)
                });
            }
            return tempCategories;
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var product = _unitofwork.Product.GetT(x => x.Id == id);
            ViewBag.categories = convertCategories();
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteData(int? id)
        {
            var product = _unitofwork.Product.GetT(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, product.ImageUrl);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                _unitofwork.Product.Delete(product);
                _unitofwork.Save();
                TempData["success"] = "Product deleted successfully!!!";
                return RedirectToAction("Index");
            }
        }
    }
}
