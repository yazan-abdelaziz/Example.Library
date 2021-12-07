using Example.Library.Contracts.Book;
using Example.Library.DataAccessSQL.Entities;
using System.Linq;

namespace Example.Library.Business.BusinessLogic.Mapping
{
    public static class BookMapping
    {
        public static BookResource ConvertToResource(this BookEntity entity)
        {
            return entity != null
                ? new BookResource
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Authors = entity.Authors?.Select(x => x.ConvertToView()).ToList()
                } : null;
        }

        public static AuthorLightResource ConvertToView(this AuthorEntity entity)
        {
            return entity != null
                ? new AuthorLightResource
                {
                    Id = entity.Id,
                    FullName = entity.FirstName + "-" + entity.LastName
                } : null;
        }

        public static BookEntity ConvertToEntity(this BookModel model, int id = 0)
        {
            return model != null
                ? new BookEntity
                {
                    Id = id,
                    Title = model.Title
                } : null;
        }
    }
}
