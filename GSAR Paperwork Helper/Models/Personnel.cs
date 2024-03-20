using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSAR_Paperwork_Helper.Models
{
    public class Personnel
    {
        public Guid ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? EmailAddress { get; set; }


        public Personnel() { ID = Guid.NewGuid(); DateOfBirth = DateTime.Now.AddYears(-18); }
    }
}
