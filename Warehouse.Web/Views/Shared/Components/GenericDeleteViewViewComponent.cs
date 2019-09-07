using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Web.Models.Components;

namespace Warehouse.Web.Views.Shared.Components
{
    public class GenericDeleteViewViewComponent : ViewComponent
    {
        public GenericDeleteViewViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(GenericViewObjectModel vm)
        {
            var result = (IViewComponentResult)View("GenericDeleteView", vm);
            return result;
        }
    }
}
