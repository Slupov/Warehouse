using Microsoft.AspNetCore.Http;

namespace Warehouse.Web.Areas.Company.Models
{
    public class CompanyCreateViewModel
    {
        public Data.Models.Company Company { get; set; }

        public IFormFile CompanyAvatar { get; set; }
    }
}
