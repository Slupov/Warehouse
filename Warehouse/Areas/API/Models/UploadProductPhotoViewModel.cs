using Microsoft.AspNetCore.Http;
using Warehouse.Data.Models;

namespace Warehouse.Web.Areas.API.Models
{
    public class UploadProductPhotoViewModel
    {
        public int ProductId { get; set; }

        public IFormFile UploadPhoto { get; set; }
    }
}
