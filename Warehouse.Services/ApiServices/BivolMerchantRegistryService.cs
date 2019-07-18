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
                const string TITLE_TAG     = "<title>";
                const string TITLE_END_TAG = "ЕИК";

                //TODO Stoyan Lupov 17 July, 2019 Replace with non-blocking function
                string htmlCode = client.DownloadString("https://tr.bivol.bg/view.php?eik=" + companyIdCode);

                int START_IDX  = htmlCode.IndexOf(TITLE_TAG) + TITLE_TAG.Length;
                int SUBSTR_LEN = htmlCode.IndexOf(TITLE_END_TAG) - START_IDX;

                htmlCode = htmlCode.Substring(START_IDX, SUBSTR_LEN).Trim();

                return htmlCode;
            }
        }
    }
}
