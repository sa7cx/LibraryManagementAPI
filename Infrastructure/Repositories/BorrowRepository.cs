using Application.Interfaces.IRepositories;
using LibraryManagementAPI.Data;
using LibraryManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BorrowRepository : IBorrowRepository
    {
        private readonly AppDbContext _context;

        public BorrowRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BorrowRecord>> GetAll()
        {
            var borrowRecords = await _context.BorrowRecords
                .Include(b => b.Book)
                .Include(b => b.Member)
                .ToListAsync();
            return borrowRecords;
        }

        public async Task<BorrowRecord> GetById(int id)
        {
            var borrowRecord = await _context.BorrowRecords
                .Include(b => b.Book)
                .Include(b => b.Member)
                .FirstOrDefaultAsync(b => b.Id == id);
            return borrowRecord;
        }

        public async Task Add(BorrowRecord borrowRecord)
        {
            await _context.BorrowRecords.AddAsync(borrowRecord);
            await _context.SaveChangesAsync();
        }

        public async Task Update(BorrowRecord borrowRecord)
        {
            _context.BorrowRecords.Update(borrowRecord);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(BorrowRecord borrowRecord)
        {
            _context.BorrowRecords.Remove(borrowRecord);
            await _context.SaveChangesAsync();
        }
    }
}

