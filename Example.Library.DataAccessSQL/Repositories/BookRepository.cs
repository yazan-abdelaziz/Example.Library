using Example.Library.DataAccessSQL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Example.Library.DataAccessSQL.Repositories
{
    public class BookRepository : IRepository<BookEntity, int>
    {
        private readonly AppDbContext _context;
        public BookRepository(IAppDbContextFactory<AppDbContext> context)
        {
            _context = context.Create();
        }

        public async Task<BookEntity> GetByIdAsync(int id)
        {
            return await _context.Books.Where(x => x.Id == id).Include(x => x.Authors).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BookEntity>> GetItemsAsync()
        {
            return await _context.Books.Include(x => x.Authors).ToListAsync();
        }

        public async Task CreateAsync(BookEntity entity)
        {
            await _context.Books.AddAsync(entity);
        }

        public async Task UpdateAsync(BookEntity entity)
        {
            _context.Books.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(BookEntity entity)
        {
            _context.Books.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<BookEntity, bool>> filters)
        {
            return await Task.Run(() => _context.Books.Where(filters).Any());
        }
    }
}
