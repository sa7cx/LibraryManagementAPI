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
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContext _context;

        public MemberRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Member>> GetAll()
        {
            var members = await _context.Members.ToListAsync();
            return members;
        }

        public Task<Member> GetById(int id)
        {
            var member = _context.Members.FirstOrDefaultAsync(m => m.MemberID == id);
            return member;
        }

        public async Task Add(Member member)
        {
           await _context.AddAsync(member);
           await _context.SaveChangesAsync();
        }

        public async Task Update(Member member)
        {
            _context.Update(member);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Member member)
        {
            _context.Remove(member);
            await _context.SaveChangesAsync();
        }

    }
}
