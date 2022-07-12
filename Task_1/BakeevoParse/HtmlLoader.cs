
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Task_1.BakeevoParse
{
    internal class HtmlLoader
    {
        readonly HttpClient client;
        readonly string url;
        public HtmlLoader(string url)
        {
            client = new HttpClient();
            this.url = url;
        }

        public async Task<string> GetSourse()
        {
            var response = client.GetAsync(url).Result;
            string source = null;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }
    }
}
