using Angular_Sample.Data;
using System.Linq;
using Util.Data;

namespace Angular_Sample.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork<SecuritiesTransactionContext> uow;

        public CompanyService(IUnitOfWork<SecuritiesTransactionContext> uow)
        {
            this.uow = uow;
        }

        public IQueryable<Company> GetCompanies()
        {
            return uow.Repository<Company>().DbSet;
        }


    }
}
