using Autofac;
using Example.Library.Business.BusinessLogic.Services;
using Example.Library.DataAccessSQL;
using Example.Library.DataAccessSQL.Entities;
using Example.Library.DataAccessSQL.Repositories;

namespace Example.Library.Business.Infrastructure
{
    public class DependencyInjectionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(t => t.Resolve<IAppDbContextFactory<AppDbContext>>().Create())
                .As<AppDbContext>()
                .InstancePerLifetimeScope();

            builder
                .RegisterGeneric(typeof(AuthorRepository))
                .As(typeof(IRepository<AuthorEntity, int>))
                .InstancePerLifetimeScope();

            builder
                .RegisterGeneric(typeof(BookRepository))
                .As(typeof(IRepository<BookEntity, int>))
                .InstancePerLifetimeScope();

            builder
                .RegisterType<BookService>()
                .As<IBookService>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<AuthorService>()
                .As<IAuthorService>()
                .InstancePerLifetimeScope();

            // Not sure
            /*builder
                .Register(t => t.Resolve<IAppDbContextFactory<AppDbContext>>().Create())
                .As<DbContext>()
                .InstancePerLifetimeScope();*/
        }
    }
}
