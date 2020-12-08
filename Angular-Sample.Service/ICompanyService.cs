using Angular_Sample.Data;
using System.Linq;

namespace Angular_Sample.Service
{
    public interface ICompanyService
    {
        IQueryable<Company> GetCompanies();
    }
}
