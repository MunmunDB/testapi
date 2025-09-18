using Core.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.API
{
    public interface IExternalService
    {
        Task<IEnumerable<FundDetails>> GetAllAsync();
    }
}