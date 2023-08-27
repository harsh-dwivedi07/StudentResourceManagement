using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using SRM.DataAccess;
using StudentResourceManagement.Repository.Implementation;
using StudentResourceManagement.Repository.IRepository;

namespace StudentResourceManagement.Controllers
{
    public class ResourceController : Controller
    {
        
        private readonly IResource _res;
        public ResourceController(IResource res)
        {
            _res = res;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult getAllResources()
        {

            try
            {
                var presentResources = _res.GetAll().ToList();
                if (presentResources.Any())
                {
                    return StatusCode(StatusCodes.Status200OK, presentResources);
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, "No Resources Present.");
                }

            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Resource creation Failed.");
            }
            }
        [HttpPost]
        public IActionResult Add(String resourceName)
        {
            try
            {
                var presentResources = _res.GetAll()
                                           .Select(x=>x.Name)
                                           .ToList();
                if (presentResources.Any() && presentResources.Contains(resourceName))
                {
                    return StatusCode(StatusCodes.Status200OK, "Resource with name is Already Present.");
                }
                _res.Add(resourceName);
                return StatusCode(StatusCodes.Status200OK, "Resource created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Resource creation Failed.");
            }
        }
    }
}
