using Example.Library.Contracts.Author;
using Example.Library.DataAccessSQL.Entities;
using System.Linq;

namespace Example.Library.Business.BusinessLogic.Mapping
{
    public static class AuthorMapping
    {
        public static AuthorResource ConvertToResource(this AuthorEntity entity)
        {
            return entity != null
                ? new AuthorResource
                {
                    Id = entity.Id,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Books = entity.Books?.Select(x => x.ConvertToView()).ToList()
                } : null;
        }

        public static BookLightResource ConvertToView(this BookEntity entity)
        {
            return entity != null
                ? new BookLightResource
                {
                    Id = entity.Id,
                    Title = entity.Title
                } : null;
        }

        public static AuthorEntity ConvertToEntity(this AuthorModel model, int id = 0)
        {
            return model != null
                ? new AuthorEntity
                {
                    Id = id,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                } : null;
        }
    }
}
