using Autofac;
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
                .RegisterGeneric(typeof(IRepository<AuthorEntity,int>))
                .As(typeof(AuthorRepository))
                .InstancePerLifetimeScope();

            builder
                .RegisterGeneric(typeof(IRepository<BookEntity,int>))
                .As(typeof(BookRepository))
                .InstancePerLifetimeScope();

            // Not sure
            /*builder
                .Register(t => t.Resolve<IAppDbContextFactory<AppDbContext>>().Create())
                .As<DbContext>()
                .InstancePerLifetimeScope();*/
        }
    }
}
