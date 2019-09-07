using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Web.Models.Components;

namespace Warehouse.Web.Views.Shared.Components
{
    public class GenericCreateViewViewComponent : ViewComponent
    {
        public GenericCreateViewViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(GenericViewObjectModel vm)
        {
            var result = (IViewComponentResult)View("GenericCreateView", vm);
            return result;
        }
    }
}
