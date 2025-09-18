namespace Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System.IO;
    using Api.DataFiles;
    using Core.API;

    [Route("api/[controller]")]
    public class FundsController : Controller
    {
        private readonly IFunds _funds;

        public FundsController(IFunds funds)
        {
            _funds = funds;
        }
        [Route("get-funds")]
        [HttpGet("{id}")]
        public IActionResult GetFunds(string id)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" )
            {
                return this.Ok(GetFundsFromFile(id));
            }
            return this.Ok(GetFundsFromRemoteService(id));
        }

        [Route("get-managerfunds")]
        public IActionResult GetManagerFunds(string manager)
        {
           if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                return this.Ok(GetManagerFundsFromFile(manager));
            }
            return this.Ok(GetManagerFundsFromRemoteService(manager));
        }

        private IEnumerable<FundDetails> GetFundsFromFile(string id)
        {
            var file = System.IO.File.ReadAllTextAsync("./DataFiles/funds.json").Result;

            var funds = JsonConvert.DeserializeObject<List<FundDetails>>(file);

            if (id != null)
            {
                return funds.Where(x => x.MarketCode == id).AsEnumerable();
            }

            return funds;       
        }

        private IEnumerable<FundDetails> GetFundsFromRemoteService(string id)
        {
            var funds = _funds.GetFundDetails(id);
            if (funds == null || !funds.Any())
            {
                return Enumerable.Empty<FundDetails>();
            }

            return (IEnumerable<FundDetails>)funds;
        }

        private IEnumerable<FundDetails> GetManagerFundsFromFile(string manager)
        {
            var file = System.IO.File.ReadAllTextAsync("./DataFiles/funds.json").Result;
            var funds = JsonConvert.DeserializeObject<List<FundDetails>>(file);
            if (manager != null)
            {
                return funds.Where(x => x.FundManager.Equals(manager, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return funds;
        }
        private IEnumerable<FundDetails> GetManagerFundsFromRemoteService(string manager)
        {
            var funds = _funds.GetManagerFunds(manager);
            if (funds == null || !funds.Any())
            {
                return Enumerable.Empty<FundDetails>();
            }
            return (IEnumerable<FundDetails>)funds;
        }
    }
}