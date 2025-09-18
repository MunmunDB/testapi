using Core.Api.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.API
{
   public interface IFunds
    {
        IEnumerable<FundDetails> GetFundDetails(string id);
        IEnumerable<FundDetails> GetManagerFunds(string manager);
    }
}
