using LibraryManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IMemberRepository
    {
        public Task<IEnumerable<Member>> GetAll();
        public Task<Member> GetById(int id);
        public Task Add(Member member);
        public Task Update(Member member);
        public Task Delete(Member member);

    }
}
