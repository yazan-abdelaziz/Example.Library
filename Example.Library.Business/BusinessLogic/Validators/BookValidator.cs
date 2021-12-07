using Example.Library.DataAccessSQL.Entities;
using Example.Library.DataAccessSQL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Example.Library.Business.BusinessLogic.Validators
{
    public interface IBookValidator
    {
        Task ValidateAuthors(List<int> authorsIds);
        Task BookExists(int id);
    }

    public class BookValidator : IBookValidator
    {
        public readonly IRepository<BookEntity, int> _bookRepository;
        public readonly IRepository<AuthorEntity, int> _authorRepository;

        public BookValidator(
            IRepository<BookEntity, int> bookRepository,
            IRepository<AuthorEntity, int> authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public async Task BookExists(int id)
        {
            Expression<Func<BookEntity, bool>> expression = x => x.Id == id;
            if(await _bookRepository.ExistsAsync(expression))
            {
                throw new InvalidOperationException($"Book With Id: {id} does not exist.");
            }
        }

        public async Task ValidateAuthors(List<int> authorsIds)
        {
            Expression<Func<AuthorEntity, bool>> expression = x => authorsIds == null || authorsIds.Contains(x.Id);
            if (await _authorRepository.ExistsAsync(expression))
            {
                throw new InvalidOperationException("Authors Ids should be for existing authors.");
            }
        }
    }
}
