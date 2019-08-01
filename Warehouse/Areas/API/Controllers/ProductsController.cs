using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Data.Models;
using Warehouse.Services;
using Warehouse.Services.Media;
using Warehouse.Web.Areas.API.Models;

namespace Warehouse.Web.Areas.API.Controllers
{
    public class ProductsController : ApiBaseController
    {
        private readonly IMediaTransferer _mediaTransferer;
        private readonly IGenericDataService<Product> _products;

        public ProductsController(IMediaTransferer mediaTransferer,
            IGenericDataService<Product> products)
        {
            _mediaTransferer = mediaTransferer;
            _products        = products;
        }

        [HttpPost]
        public async Task<IActionResult> UploadProductPhoto([FromForm]UploadProductPhotoViewModel vm)
        {
            var product = await _products.GetSingleOrDefaultAsync(x => x.Id == vm.ProductId);

            if (await _mediaTransferer.UploadProductPhoto(product, vm.UploadPhoto))
            {
                return Ok((await _mediaTransferer.GetProductPhotosPaths(product)).Last());
            }

            return Forbid();
        }
    }
}