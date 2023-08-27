using Microsoft.AspNetCore.Mvc;
using SRM.Models;

namespace StudentResourceManagement.Repository.IRepository
{
    public interface IResource
    {
        List<ResourceModel> GetAll();
        bool Add(String name);
        IActionResult Remove(String name);
    }
}
