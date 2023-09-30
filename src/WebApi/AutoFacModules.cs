using Application.Queries;
using Application.Repositories;
using Autofac;
using Infrastructure.GlobalExceptionHandler;
using MediatR;
using System.Reflection;

namespace WebApi
{
    /// <summary>
    /// AppModule
    /// </summary>
    public class AutoFacModules : Autofac.Module
    {
        /// <summary>
        /// Load all dependencies
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            RegisterRepositories(builder);

/*            builder.RegisterAssemblyTypes(typeof(AddItemCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .InstancePerDependency();*/

            builder.RegisterAssemblyTypes(typeof(GetItemsQuery).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .InstancePerDependency();

            builder.RegisterType<GlobalExceptionHandler>().InstancePerLifetimeScope();
        }

        private void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<ItemsRepository>().As<IItemsRepository>()
                .InstancePerLifetimeScope();

/*            builder.RegisterType<ItemCategoryRepository>().As<IItemCategoryRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RecipeRepository>().As<IRecipeRepository>()
                .InstancePerLifetimeScope();*/
        }
    }
}
