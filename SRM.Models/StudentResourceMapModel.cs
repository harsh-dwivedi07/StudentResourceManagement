using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRM.Models
{
    public class StudentResourceMapModel
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        [ForeignKey("StudentId")]
        public StudentModel Student { get; set; }
        public Guid ResourceId { get; set; }
        [ForeignKey("ResourceId")]
        public ResourceModel Resource { get; set; }

        public DateTime ExpiryDate { get; set; }

    }
    public class StudentResourceMapModelProxy
    {
       public string StudentName { get; set; }
        public string ResourceName { get; set;}
        public string AllocatedTill { get; set;}

    }
}
