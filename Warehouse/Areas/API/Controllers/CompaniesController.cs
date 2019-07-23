using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Services;
using Warehouse.Services.ApiServices;
using Warehouse.Web.Areas.API.Models;

namespace Warehouse.Web.Areas.API.Controllers
{
    [Area("Api")]
    public class CompaniesController : Controller
    {
        private IGenericDataService<Data.Models.Company> _companies;
        private IMerchantRegistryService _merchantRegistry;

        public CompaniesController(IGenericDataService<Data.Models.Company> companies, IMerchantRegistryService merchantRegistry)
        {
            _companies        = companies;
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