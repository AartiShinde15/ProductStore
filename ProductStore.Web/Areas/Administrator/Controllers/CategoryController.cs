using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductStore.DataAccess.ViewModels;
using ProductStore.DataAccess.Repositories;

namespace ProductStore.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class CategoryController : Controller
    {
        private IUnitOfWork _unitofwork;
        private IWebHostEnvironment _hostEnvironment;

        public CategoryController(IUnitOfWork unitofwork, IWebHostEnvironment hostEnvironment)
        {
            _unitofwork = unitofwork;
            _hostEnvironment = hostEnvironment;
        }

     
        public IActionResult Index()
        {
            CategoryVM categoryVM = new CategoryVM();
            categoryVM.categories = _unitofwork.Category.GetAll();
            return View(categoryVM);
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
        public IActionResult CreateUpdate(int? id)
        {
            CategoryVM categoryVM = new CategoryVM();
            if (id == null || id == 0)
            {
                return View(categoryVM);
            }
            else
            {
                categoryVM.category = _unitofwork.Category.GetT(x => x.Id == id);
                if (categoryVM.category == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(categoryVM);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(CategoryVM categoryVM)
        {
            if (ModelState.IsValid)
            {                
                if (categoryVM.category.Id == 0)
                {
                    _unitofwork.Category.Add(categoryVM.category);
                    TempData["success"] = "Category created successfully!!!";
                }
                else
                {
                    _unitofwork.Category.Update(categoryVM.category);
                    TempData["success"] = "Category updated successfully!!!";
                }
                _unitofwork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View(categoryVM);
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _unitofwork.Category.GetT(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteData(int? id)
        {
            var category = _unitofwork.Category.GetT(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                _unitofwork.Category.Delete(category);
                _unitofwork.Save();
                TempData["success"] = "Category deleted successfully!!!";
                return RedirectToAction("Index");
            }
        }
    }
}
