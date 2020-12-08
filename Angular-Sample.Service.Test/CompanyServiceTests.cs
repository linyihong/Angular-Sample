using Angular_Sample.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using Util.Data;

namespace Angular_Sample.Service.Test
{
    [TestClass]
    public class CompanyServiceTests
    {
        [TestMethod]
        public void TestCompanyService()
        {

            var uow = Substitute.For<IUnitOfWork<SecuritiesTransactionContext>>();
            var companyService = new CompanyService(uow);


            Assert.IsNotNull(companyService);
        }

        [TestMethod]
        public void TestGetCompanies()
        {
            var uow = Substitute.For<IUnitOfWork<SecuritiesTransactionContext>>();
            var companyService = new CompanyService(uow);

            var name = "Test";

            SetUnitOfWorkRepository(uow, new List<Company> {
            new Company{
                ChName = name
            }

            });

            Assert.AreEqual(companyService.GetCompanies().First().ChName, name);
        }

        private IUnitOfWork<TDbContext> SetUnitOfWorkRepository<TDbContext, TEntity>(IUnitOfWork<TDbContext> uow, IEnumerable<TEntity> entities)
            where TDbContext : DbContext where TEntity : class
        {
            var queryEntities = entities.AsQueryable();

            SetUnitOfWorkRepository(uow, queryEntities);

            return uow;
        }

        private IUnitOfWork<TDbContext> SetUnitOfWorkRepository<TDbContext, TEntity>(IUnitOfWork<TDbContext> uow, IQueryable<TEntity> entities)
            where TDbContext : DbContext where TEntity : class
        {
            var mockDbSet = MockDbSet(entities);
            var contexProperty = uow.Context.GetType().GetProperty(typeof(TEntity).Name);

            contexProperty.SetValue(uow.Context, mockDbSet);

            uow.Repository<TEntity>().GetAll().Returns(mockDbSet);
            uow.Repository<TEntity>().DbSet.Returns(mockDbSet);

            return uow;
        }


        private DbSet<T> MockDbSet<T>(IQueryable<T> data) where T : class
        {
            var mockDbSet = Substitute.For<DbSet<T>, IQueryable<T>>();

            ((IQueryable<T>)mockDbSet).Provider.Returns(data.Provider);
            ((IQueryable<T>)mockDbSet).Expression.Returns(data.Expression);
            ((IQueryable<T>)mockDbSet).ElementType.Returns(data.ElementType);
            ((IQueryable<T>)mockDbSet).GetEnumerator().Returns(data.GetEnumerator());


            return mockDbSet;
        }
    }
}
