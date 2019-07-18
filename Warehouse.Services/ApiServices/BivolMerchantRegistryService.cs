using System.Net;
using System.Threading.Tasks;

namespace Warehouse.Services.ApiServices
{
    public class BivolMerchantRegistryService : IMerchantRegistryService
    {
        public async Task<string> GetCompanyName(string companyIdCode)
        {
            using (WebClient client = new WebClient())
            {
                //TODO Stoyan Lupov 17 July, 2019 Replace with non-blocking function
                string htmlCode = client.DownloadString("https://tr.bivol.bg/view.php?eik=" + companyIdCode);

                return htmlCode;
            }

            return string.Empty;
        }
    }
}
