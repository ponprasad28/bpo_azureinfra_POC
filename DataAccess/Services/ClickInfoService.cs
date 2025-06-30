using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services
{
    public class ClickInfoService : IClickInfoService
    {
        private readonly AppDbContext _context;

        public ClickInfoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClickInfo>> GetAllAsync() =>
            await _context.ClickInfos.ToListAsync();

        public async Task<ClickInfo?> GetByIdAsync(int id) =>
            await _context.ClickInfos.FindAsync(id);

        public async Task AddAsync(ClickInfo click)
        {
            _context.ClickInfos.Add(click);
            await _context.SaveChangesAsync();
        }
    }
}
