using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SRM.DataAccess;
using SRM.Models;
using StudentResourceManagement.Repository.IRepository;

namespace StudentResourceManagement.Repository.Implementation
{
    public class Student:IStudent
    {
        private readonly ApplicationDbContext _context;
        public Student(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public bool Add(StudentModel entity)
        {
           
            _context.Students.Add(entity);
            _context.SaveChanges();
            return true;
        }

        public List<StudentModel> GetAll()
        {
            return _context.Students.ToList();
        }

        public IActionResult Remove(string name)
        {
            throw new NotImplementedException();
        }
        public bool AddMapping(Guid studentId, Guid resourceId,DateTime exp)
        {
            StudentResourceMapModel smp=new StudentResourceMapModel();
            smp.Id=Guid.NewGuid();
            smp.StudentId=studentId;
            smp.ResourceId=resourceId;
            smp.ExpiryDate = exp;
            _context.StudentResourceMap.Add(smp);
            _context.SaveChanges(true);
            return true;
        }
        public List<StudentResourceMapModel> GetAllMapping()
        {
            var obj=_context.StudentResourceMap
                             .Include(srm => srm.Student)
                             .Include(srm => srm.Resource)
                            .ToList();
            return obj;
        }
    }
}
