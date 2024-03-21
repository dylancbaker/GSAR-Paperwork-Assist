using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<Personnel> students { get; set; } = new ObservableCollection<Personnel>();
        public ObservableCollection<Course> courses { get; set; } = new ObservableCollection<Course>();
        public string? FilePath { get; set; }

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

          
        }

        public void SetUpNewProgram()
        {
            courses = new ObservableCollection<Course>(CourseTools.GetBlankCourses());

        }
    }

    public static class GSARProgramTools
    {
       
    }
}
