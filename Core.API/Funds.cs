using Core.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;

namespace Core.API
{
    public class Funds : IFunds
    {
        private readonly IService _service;

        public Funds(IService service)
        {
            
            _service = service;
        }

        public IEnumerable<FundDetails> GetFundDetails(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Guid idGuid = Guid.Empty;

                if (!Guid.TryParse(id, out idGuid))
                {
                    throw new ArgumentException("Invalid GUID format", nameof(id));
                }

                var allFunds = GetAllFunds();
                return allFunds.Where(x => x.Id == idGuid);
            }
            return GetAllFunds();
        }
        public IEnumerable<FundDetails> GetManagerFunds(string manager)
        {
            var allFunds = GetAllFunds();
            var managerFunds = allFunds.Where(x => x.FundManager.Equals(manager, StringComparison.OrdinalIgnoreCase)).ToList();
            return managerFunds;
        }

        private IEnumerable<FundDetails> GetAllFunds()
        {
            return _service.GetAllAsync().Result;
        }
    }
}
