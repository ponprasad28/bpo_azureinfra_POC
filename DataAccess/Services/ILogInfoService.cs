using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Services
{
    public interface ILogInfoService
    {
        Task<IEnumerable<LogInfo>> GetAllAsync();
        Task<LogInfo?> GetByIdAsync(int id);
        Task AddAsync(LogInfo log);
    }
}
