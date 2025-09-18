using Core.Api.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.API
{
    public class Service : IService
    {
        private readonly string _url;

        public Service()
        {
            // Remote URL to fetch fund details  --- this is a static URL for the purpose of this test
            _url = "https://raw.githubusercontent.com/StarlingBank/StarlingTechTest/master/data/funds.json";
        }

        public async Task<IEnumerable<FundDetails>> GetAllAsync()
        {
            try
            {
                return await FetchFromRemoteAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new ApplicationException("An error occurred while fetching fund details.", ex);
            }

        }
        private async Task<IEnumerable<FundDetails>> FetchFromRemoteAsync()
        {
            using (var http = new HttpClient())
            {
                // 1. Call the remote URL
                var response = await http.GetAsync(_url);
                response.EnsureSuccessStatusCode();

                // 2. Read the JSON payload
                var json = await response.Content.ReadAsStringAsync();

                // 3. Deserialize into a List<YourDto>
                var list = JsonConvert.DeserializeObject<List<FundDetails>>(json);

                return list;
            }
           
        }
    }
}
