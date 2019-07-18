using System.Threading.Tasks;

namespace Warehouse.Services.ApiServices
{
    public interface IMerchantRegistryService
    {
        Task<string> GetCompanyName(string companyIdCode);
    }
}
