using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStore.DataAccess.ViewModels
{
    public class ProductVM
    {
        public Product product { get; set; } = new Product();
        [ValidateNever]
        public IEnumerable<Product> products { get; set; } = new List<Product>();
        [ValidateNever]
        public IEnumerable<SelectListItem> categories { get; set; }
    }
}
