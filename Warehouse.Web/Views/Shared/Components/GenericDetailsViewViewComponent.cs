using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Web.Models.Components;

namespace Warehouse.Web.Views.Shared.Components
{
    public class GenericDetailsViewViewComponent : ViewComponent
    {
        public GenericDetailsViewViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(GenericViewObjectModel vm)
        {
            var result = (IViewComponentResult) View("GenericDetailsView", vm);
            return result;
        }
    }
}