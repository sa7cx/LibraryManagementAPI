using LibraryManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IBorrowRepository
    {
        public Task<IEnumerable<BorrowRecord>> GetAll();
        public Task<BorrowRecord> GetById(int id);
        public Task Add(BorrowRecord borrowRecord);
        public Task Update(BorrowRecord borrowRecord);
        public Task Delete(BorrowRecord borrowRecord);
    }
}
