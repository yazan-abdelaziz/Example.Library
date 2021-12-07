using Example.Library.Business.BusinessLogic.Mapping;
using Example.Library.Business.BusinessLogic.Validators;
using Example.Library.Contracts.Author;
using Example.Library.DataAccessSQL.Entities;
using Example.Library.DataAccessSQL.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Library.Business.BusinessLogic.Services
{
    public interface IAuthorService
    {
        Task<List<AuthorResource>> GetAllAuthors();
        Task<AuthorResource> GetAuthorById(int id);
        Task<AuthorResource> CreateAuthor(AuthorModel model);
        Task<AuthorResource> UpdateAuthor(int id, AuthorModel model);
        Task DeleteAuthor(int id);
    }

    public class AuthorService : IAuthorService
    {
        private readonly IRepository<AuthorEntity, int> _repository;
        private readonly IRepository<BookEntity, int> _bookRepository;
        private readonly IAuthorValidator _validator;

        public AuthorService(
            IRepository<AuthorEntity, int> repository,
            IRepository<BookEntity, int> bookRepository,
            IAuthorValidator validator)
        {
            _repository = repository;
            _bookRepository = bookRepository;
            _validator = validator;
        }

        public async Task<List<AuthorResource>> GetAllAuthors()
        {
            return (await _repository.GetItemsAsync())?.Select(x => x.ConvertToResource()).ToList();
        }

        public async Task<AuthorResource> GetAuthorById(int id)
        {
            return (await _repository.GetByIdAsync(id))?.ConvertToResource();
        }

        public async Task<AuthorResource> CreateAuthor(AuthorModel model)
        {
            await _validator.ValidateBooks(model.BooksIds);
            var entity = model.ConvertToEntity();
            await FillBooks(entity, model.BooksIds);
            return (await _repository.CreateAsync(entity)).ConvertToResource();
        }

        public async Task<AuthorResource> UpdateAuthor(int id, AuthorModel model)
        {
            await _validator.ValidateBooks(model.BooksIds);
            var entity = model.ConvertToEntity(id);
            await FillBooks(entity, model.BooksIds);
            return (await _repository.UpdateAsync(entity)).ConvertToResource();
        }

        public async Task DeleteAuthor(int id)
        {
            await _validator.AuthorExists(id);
            var entity = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(entity);
        }

        #region Private Methods

        private async Task FillBooks(AuthorEntity entity, List<int> booksIds)
        {
            var books = booksIds.Select(async x => await _bookRepository.GetByIdAsync(x));
            entity.Books = (await Task.WhenAll(books))?.ToList();
        }

        #endregion
    }
}
