using System;
using System.Threading.Tasks;
using Autofac;
using EF_Task_Standard;
using EF_Task_Standard.Scope;
using NUnit.Framework;

namespace EntityFrameworkUnitTests
{
    [TestFixture]
    public class UnitTests
    {
        /// <summary>
        /// • Выборка списка заказов, по одной категории (т.е. те заказы, в которые включены продукты определенной категории)
        /// • Выборка должна включать Список детальных строк 
        /// o Имя заказчика 
        /// o Имена продуктов
        /// </summary>
        [Test]
        public async Task GetListOfOrdersWithProductsAndCustomersByCategory()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<DbScopeModule>();
            var container = containerBuilder.Build();
            var scopeCreator = container.Resolve<IScopeCreator>();
            var contextLocator = container.Resolve<IContextLocator<NorthwindContext>>();
            var globalGuidOne = Guid.Empty;
            var globalGuidTwo = Guid.Empty;

            var firstTask = Task.Run(async () =>
            {
                using (scopeCreator.CreateReadonly())
                {
                    var identity = contextLocator.GetCurrentDbContext().Identity;
                    var identitySecond = contextLocator.GetCurrentDbContext().Identity;
                    var identityThird = contextLocator.GetCurrentDbContext().Identity;
                    var findAsync = await contextLocator.GetCurrentDbContext().Categories.FindAsync(1).ConfigureAwait(false);
                    var identityFourth = contextLocator.GetCurrentDbContext().Identity;
                    var identityFromAnotherThread = await Task.Run(() => contextLocator.GetCurrentDbContext().Identity).ConfigureAwait(false);
                    globalGuidOne = identity;
                    Assert.That(findAsync, Is.Not.Null);
                    Assert.That(identity, Is
                        .EqualTo(identitySecond).And
                        .EqualTo(identityThird).And
                        .EqualTo(identityFourth).And
                        .EqualTo(identityFromAnotherThread));
                }
            });
            var secondTask = Task.Run(() =>
            {
                using (scopeCreator.CreateReadonly())
                {
                    var identity = contextLocator.GetCurrentDbContext().Identity;
                    var identitySecond = contextLocator.GetCurrentDbContext().Identity;
                    var identityThird = contextLocator.GetCurrentDbContext().Identity;
                    globalGuidTwo = identity;
                    Assert.That(identity, Is.EqualTo(identitySecond).And.EqualTo(identityThird));
                }
            });

            await Task.WhenAll(firstTask, secondTask).ConfigureAwait(false);
            Assert.That(globalGuidOne, Is.Not.SameAs(globalGuidTwo));

            container.Resolve<IProductCategoryService>().GetProductInfoForCategory(2);
        }
    }
}
