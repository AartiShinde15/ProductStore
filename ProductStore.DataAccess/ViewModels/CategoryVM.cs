using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductStore.Models;

namespace ProductStore.DataAccess.ViewModels
{
    public class CategoryVM
    {
        [ValidateNever]
        public Category category { get; set; } = new Category();
        [ValidateNever]
        public IEnumerable<Category> categories { get; set; } = new List<Category>();
    }
}
