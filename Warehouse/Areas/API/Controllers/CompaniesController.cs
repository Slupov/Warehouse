using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Services.ApiServices;

namespace Warehouse.Web.Areas.API.Controllers
{
    public class CompaniesController : ApiBaseController
    {
        private IMerchantRegistryService _merchantRegistry;

        public CompaniesController(IMerchantRegistryService merchantRegistry)
        {
            _merchantRegistry = merchantRegistry;
        }

        //api/companies/getname
        [HttpPost]
        public async Task<IActionResult> GetName([FromBody] string id)
        {
            string companyName = await _merchantRegistry.GetCompanyName(id);

            return Ok(companyName);
        }
    }
}