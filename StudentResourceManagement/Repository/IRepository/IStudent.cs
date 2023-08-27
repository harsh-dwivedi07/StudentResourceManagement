using Microsoft.AspNetCore.Mvc;
using SRM.Models;
using StudentResourceManagement.Repository.Implementation;

namespace StudentResourceManagement.Repository.IRepository
{
    public interface IStudent
    {
        List<StudentModel> GetAll();
        bool Add(StudentModel entity);
        bool AddMapping(Guid studentId, Guid resourceId, DateTime exp);
        List<StudentResourceMapModel> GetAllMapping();
        IActionResult Remove(String name);
    }
}
