using Core.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.API
{
    public interface IService
    {
        Task<IEnumerable<FundDetails>> GetAllAsync();
    }
}