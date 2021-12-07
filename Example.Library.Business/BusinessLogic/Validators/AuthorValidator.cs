using Example.Library.DataAccessSQL.Entities;
using Example.Library.DataAccessSQL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Example.Library.Business.BusinessLogic.Validators
{
    public interface IAuthorValidator
    {
        Task ValidateBooks(List<int> booksIds);
        Task AuthorExists(int id);
    }

    public class AuthorValidator : IAuthorValidator
    {
        private readonly IRepository<BookEntity, int> _bookRepository;
        private readonly IRepository<AuthorEntity, int> _authorRepository;

        public AuthorValidator(
            IRepository<BookEntity, int> bookRepository,
            IRepository<AuthorEntity, int> authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public async Task AuthorExists(int id)
        {
            Expression<Func<AuthorEntity, bool>> expression = x => x.Id == id;
            if (await _authorRepository.ExistsAsync(expression))
            {
                throw new InvalidOperationException($"Author With Id: {id} does not exist.");
            }
        }

        public async Task ValidateBooks(List<int> booksIds)
        {
            Expression<Func<BookEntity, bool>> expression = x => booksIds == null || booksIds.Contains(x.Id);
            if(await _bookRepository.ExistsAsync(expression))
            {
                throw new InvalidOperationException("Books Ids should be for existing books.");
            }
        }
    }
}
