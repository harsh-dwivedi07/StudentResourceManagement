using Microsoft.AspNetCore.Mvc;
using SRM.Models;
using StudentResourceManagement.Repository.IRepository;

namespace StudentResourceManagement.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudent _stu;
        public StudentController(IStudent stu)
        {
            _stu = stu;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(String studentName,Guid resourceId, DateTime tillDate)
        {
            try
            {
                var presentResources = _stu.GetAll().Select(x=>x.Name).ToList();
                if (presentResources.Any() && presentResources.Contains(studentName))
                {
                    return StatusCode(StatusCodes.Status200OK, "Student with name is Already Present.");
                }
                StudentModel studentModel = new StudentModel();
                studentModel.Name = studentName;
                studentModel.Id = Guid.NewGuid();
                _stu.Add(studentModel);
                _stu.AddMapping(studentModel.Id, resourceId, tillDate);
                return StatusCode(StatusCodes.Status200OK, "Student created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Student creation Failed.");
            }
        }
        public List<StudentResourceMapModelProxy> GetAllMapping() { 
        var obj=_stu.GetAllMapping().ToList();
            var entity=obj.Select(x=> new StudentResourceMapModelProxy{
                StudentName=x.Student.Name,
                ResourceName=x.Resource.Name,
                AllocatedTill= DateTime.Parse(x.ExpiryDate.ToString()).ToString("d-MMM-yy")
            }).ToList();
            return entity;
        }

        public IActionResult AddMapping(Guid studentId,Guid resourceId,DateTime tillDate)
        {
            try
            {
                _stu.AddMapping(studentId, resourceId, tillDate);
                return StatusCode(StatusCodes.Status200OK, "Mapping created successfully.");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Mapping creation Failed.");
            }
        }

        public IActionResult getAllStudents()
        {

            try
            {
                var presentResources = _stu.GetAll().ToList();
                if (presentResources.Any())
                {
                    return StatusCode(StatusCodes.Status200OK, presentResources);
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, "No Resources Present.");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Resource creation Failed.");
            }
        }
    }
}
