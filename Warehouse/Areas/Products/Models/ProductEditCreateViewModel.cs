using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Warehouse.Data.Models;

namespace Warehouse.Web.Areas.Products.Models
{
    public class ProductEditCreateViewModel
    {
        public Product Product { get; set; }

        public List<SelectListItem> Places { set; get; }

        public List<SelectListItem> MeasureUnits { set; get; }

        public List<SelectListItem> ProductGroups { set; get; }
    }
}
