using Example.Library.DataAccessSQL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Example.Library.DataAccessSQL.Repositories
{
    public class AuthorRepository : IRepository<AuthorEntity, int>
    {
        private readonly AppDbContext _context;
        public AuthorRepository(IAppDbContextFactory<AppDbContext> contextFactory)
        {
            _context = contextFactory.Create();
        }
        public async Task<AuthorEntity> GetByIdAsync(int id)
        {
            return await _context.Authors.Where(x => x.Id == id).Include(x => x.Books).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AuthorEntity>> GetItemsAsync()
        {
            return await _context.Authors.Include(x => x.Books).ToListAsync();
        }

        public async Task<AuthorEntity> CreateAsync(AuthorEntity entity)
        {
            await _context.Authors.AddAsync(entity);
            return entity;
        }

        public async Task<AuthorEntity> UpdateAsync(AuthorEntity entity)
        {
            _context.Authors.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(AuthorEntity entity)
        {
            _context.Authors.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<AuthorEntity, bool>> filters)
        {
            return await Task.Run(() => _context.Authors.Where(filters).Any());
        }
    }
}
