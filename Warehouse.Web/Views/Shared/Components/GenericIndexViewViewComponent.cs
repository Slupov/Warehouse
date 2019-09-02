using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Web.Models;
using Warehouse.Web.Models.Components;

namespace Warehouse.Web.Views.Shared.Components
{
    public class GenericIndexViewViewComponent : ViewComponent
    {
        public GenericIndexViewViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(GenericViewObjectModel vm)
        {
            var result = (IViewComponentResult)View("GenericIndexView", vm);
            return result;
        }
    }
}
