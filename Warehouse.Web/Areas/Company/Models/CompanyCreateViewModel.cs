using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Warehouse.Web.Areas.Company.Models
{
    public class CompanyCreateViewModel
    {
        public Data.Models.Company Company { get; set; }

        [Display(Prompt = "Лого на компанията")]
        public IFormFile CompanyAvatar { get; set; }
    }
}
