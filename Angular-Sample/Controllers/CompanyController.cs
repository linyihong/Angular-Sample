using Angular_Sample.Service;
using Microsoft.AspNetCore.Mvc;

namespace Angular_Sample.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService companyService;

        public CompanyController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }


        [HttpGet]
        public IActionResult GetCompanies()
        {
            return Json(companyService.GetCompanies());
        }
    }
}
