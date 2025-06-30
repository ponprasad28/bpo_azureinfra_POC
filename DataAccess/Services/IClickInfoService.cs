using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Services
{
    public interface IClickInfoService
    {

        Task<IEnumerable<ClickInfo>> GetAllAsync();
        Task<ClickInfo?> GetByIdAsync(int id);
        Task AddAsync(ClickInfo click);


    }
}
