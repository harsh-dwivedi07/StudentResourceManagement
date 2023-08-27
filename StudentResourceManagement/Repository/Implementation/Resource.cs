using Microsoft.AspNetCore.Mvc;
using SRM.DataAccess;
using SRM.Models;
using StudentResourceManagement.Repository.IRepository;

namespace StudentResourceManagement.Repository.Implementation
{
    public class Resource : IResource
    {
        private readonly ApplicationDbContext _context;
        public Resource(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(string name)
        {
            ResourceModel resourceModel = new ResourceModel();
            resourceModel.Name = name;
            resourceModel.Id= Guid.NewGuid();
            _context.Resources.Add(resourceModel);
            _context.SaveChanges();
            return true;
        }

        public List<ResourceModel> GetAll()
        {
            return _context.Resources.ToList();
        }

        public IActionResult Remove(string name)
        {
            throw new NotImplementedException();
        }
    }
}
