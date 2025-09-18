using Core.Api.Entities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Core.API
{
    public class ExternalService : IExternalService
    {
        private readonly string _url;
        private static readonly HttpClient _httpClient;
        //private readonly AzureServiceTokenProvider _tokenProvider;  

        public ExternalService(HttpClient http, IOptions<ExternalApiOptions> opts)
        {
            // Remote URL to fetch fund details  --- this is a static URL for the purpose of this test
            _url = opts.Value.Url;
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
                //TO Do
                // To use a azure token service , uncomment the following lines
                //var accessToken = await _tokenProvider.GetAccessTokenAsync(_scope);
                //_http.DefaultRequestHeaders.Authorization =
                //    new AuthenticationHeaderValue("Bearer", accessToken);
                
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
