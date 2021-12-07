using Example.Library.Business.BusinessLogic.Mapping;
using Example.Library.Business.BusinessLogic.Validators;
using Example.Library.Contracts.Book;
using Example.Library.DataAccessSQL.Entities;
using Example.Library.DataAccessSQL.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Library.Business.BusinessLogic.Services
{
    public interface IBookService
    {
        Task<List<BookResource>> GetAllBooks();
        Task<BookResource> GetBookById(int id);
        Task<BookResource> CreateBook(BookModel model);
        Task<BookResource> UpdateBook(int id, BookModel model);
        Task DeleteBook(int id);
    }

    public class BookService : IBookService
    {
        private readonly IRepository<BookEntity, int> _repository;
        private readonly IRepository<AuthorEntity, int> _authorRepository;
        private readonly IBookValidator _validator;

        public BookService(
            IRepository<BookEntity, int> repository,
            IRepository<AuthorEntity, int> authorRepository,
            IBookValidator validator)
        {
            _repository = repository;
            _authorRepository = authorRepository;
            _validator = validator;
        }

        public async Task<List<BookResource>> GetAllBooks()
        {
            return (await _repository.GetItemsAsync())?.Select(x => x.ConvertToResource()).ToList();
        }

        public async Task<BookResource> GetBookById(int id)
        {
            return (await _repository.GetByIdAsync(id))?.ConvertToResource();
        }

        public async Task<BookResource> CreateBook(BookModel model)
        {
            await _validator.ValidateAuthors(model.AuthorsIds);
            var entity = model.ConvertToEntity();
            await FillAuthors(entity, model.AuthorsIds);
            return (await _repository.CreateAsync(entity)).ConvertToResource();
        }

        public async Task<BookResource> UpdateBook(int id, BookModel model)
        {
            await _validator.ValidateAuthors(model.AuthorsIds);
            var entity = model.ConvertToEntity();
            await FillAuthors(entity, model.AuthorsIds);
            return (await _repository.UpdateAsync(entity)).ConvertToResource();
        }

        public async Task DeleteBook(int id)
        {
            await _validator.BookExists(id);
            var entity = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(entity);
        }

        #region Private Methods

        private async Task FillAuthors(BookEntity entity, List<int> authorsIds)
        {
            var authors = authorsIds.Select(async x => await _authorRepository.GetByIdAsync(x));
            entity.Authors = (await Task.WhenAll(authors))?.ToList();
        }

        #endregion
    }
}
