using LibraryManagementAPI.Data;
using LibraryManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
           var categories = await _context.Categories.Select(c=>new Category
           {
               CategoryId = c.CategoryId,
               Name = c.Name,
               Description = c.Description,
           }).OrderBy(c => c.Name).ToListAsync();
           return categories;
        }

        public async Task<Category> GetById(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            return category;
        }

        public async Task<Category> Add(Category category)
        {
            await _context.Categories.AddAsync(category);
            _context.SaveChanges();
            return category;
        }


        public Category Update(Category category)
        {
            _context.Update(category);
            _context.SaveChanges();
            return category;
        }

        public Category Delete(Category category)
        {
            _context.Remove(category);
            _context.SaveChanges();
            return category;
        }

        public async Task<bool> IsValid(int id)
        {
            return await _context.Categories.AnyAsync(c => c.CategoryId == id);
        }


    }
}
