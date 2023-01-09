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

    public class FundsController : Controller
    {
        [Route("get-funds")]
        public IActionResult GetFunds(string id)
        {
            var file = System.IO.File.ReadAllTextAsync("./DataFiles/funds.json").Result;

            var funds = JsonConvert.DeserializeObject<List<FundDetails>>(file);

            if (id != null)
            {
                return this.Ok(funds.Single(x => x.MarketCode == id));
            }
            
            return this.Ok(funds);
        }

        [Route("get-managerfunds")]
        public IActionResult GetManagerFunds(string manager)
        {
            var file = System.IO.File.ReadAllTextAsync("./DataFiles/funds.json").Result;

            var funds = JsonConvert.DeserializeObject<List<FundDetails>>(file);

            return this.Ok(funds.Where(x => x.Name == manager));
        }

    }
}