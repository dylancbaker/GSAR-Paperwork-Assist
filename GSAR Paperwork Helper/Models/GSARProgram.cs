using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSAR_Paperwork_Helper.Models
{
    public class GSARProgram
    {
        public Guid ProgramID { get; set; }
        public Guid SARGroupID { get; set; }
        public string? SARGroupName { get; set; }
        public string? LeadInstructor { get; set; }
        public string? Email { get; set; }
        public string? MailingAddress { get; set; }
        public string? Phone { get; set; }
        public DateTime ProgramPlanDate { get; set; }
        public List<Personnel> students { get; set; } = new List<Personnel>();
        public List<Course> courses { get; set; } = new List<Course>();


        public GSARProgram()
        {
            ProgramID = Guid.NewGuid();
            if (Properties.Settings.Default.DefaultGroup != Guid.Empty)
            {
                Organization org = OrganizationTools.GetOrganization(Properties.Settings.Default.DefaultGroup);
                if (org != null) { SARGroupName = org.OrganizationName; SARGroupID = org.OrganizationID; }

            }
            ProgramPlanDate = DateTime.Now;
            LeadInstructor = Properties.Settings.Default.DefaultInstructor;

            courses = CourseTools.GetBlankCourses();
        }
    }

    public static class GSARProgramTools
    {
        public static void AddStudent(this GSARProgram program, Personnel student)
        {
            if(!program.students.Any(o=>o.ID == student.ID))
            {
                program.students.Add(student);
                program.students = program.students.OrderBy(o=>o.LastName).ThenBy(o=>o.FirstName).ToList();
            }
        }
    }
}
