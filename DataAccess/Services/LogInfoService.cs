using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services
{
    public class LogInfoService: ILogInfoService
    {
        private readonly AppDbContext _context;

        public LogInfoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LogInfo>> GetAllAsync() =>
            await _context.LogInfos.ToListAsync();

        public async Task<LogInfo?> GetByIdAsync(int id) =>
            await _context.LogInfos.FindAsync(id);

        public async Task AddAsync(LogInfo log)
        {
            _context.LogInfos.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
