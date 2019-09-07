using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Web.Models.Components;

namespace Warehouse.Web.Views.Shared.Components
{
    public class GenericEditViewViewComponent : ViewComponent
    {
        public GenericEditViewViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(GenericViewObjectModel vm)
        {
            var result = (IViewComponentResult)View("GenericEditView", vm);
            return result;
        }
    }
}
