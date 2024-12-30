using GSAR_Paperwork_Helper.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GSAR_Paperwork_Helper.Models
{
    public class GSARProgram : INotifyPropertyChanged
    {
        private Guid _ProgramID;
        private Guid _SARGroupID;
        private string? _SARGroupName;
        private string? _LeadInstructor;
        private string? _Email;
        private string? _MailingAddress;
        private string? _Phone;
        private DateTime _ProgramPlanDate;
        private BindingList<Personnel> _Students = new BindingList<Personnel>();
        private BindingList<Course> _Courses = new BindingList<Course>();

        public Guid ProgramID { get => _ProgramID; set { _ProgramID = value; OnPropertyChanged(nameof(ProgramID)); } }
        public Guid SARGroupID { get => _SARGroupID; set { _SARGroupID = value; OnPropertyChanged(nameof(SARGroupID)); } }
        public string? SARGroupName { get => _SARGroupName; set { _SARGroupName = value; OnPropertyChanged(nameof(SARGroupName)); } }
        public string? LeadInstructor { get => _LeadInstructor; set { _LeadInstructor = value; OnPropertyChanged(nameof(LeadInstructor)); } }
        public string? Email { get => _Email; set { _Email = value; OnPropertyChanged(nameof(Email)); } }
        public string? MailingAddress { get => _MailingAddress; set { _MailingAddress = value; OnPropertyChanged(nameof(MailingAddress)); } }
        public string? Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(nameof(Phone)); } }
        public DateTime ProgramPlanDate { get => _ProgramPlanDate; set { _ProgramPlanDate = value; OnPropertyChanged(nameof(ProgramPlanDate)); } }
        public BindingList<Personnel> Students { get => _Students; set { _Students = value; OnPropertyChanged(nameof(Students)); } }
        public BindingList<Course> Courses { get => _Courses; set { _Courses = value; OnPropertyChanged(nameof(Courses)); } }
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
            Students.ListChanged += Students_ListChanged;
            Courses.ListChanged += Courses_ListChanged;
        }

        private void Courses_ListChanged(object? sender, ListChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Courses));
        }

        private void Students_ListChanged(object? sender, ListChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Students));
        }

        public void SetUpNewProgram()
        {
            Courses = new BindingList<Course>(CourseTools.GetBlankCourses());

        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

      
    }

    public static class GSARProgramTools
    {
        public static bool StudentPassedCourse(this GSARProgram program, Guid CourseID, Guid PersonID)
        {
            if (program.Courses.Any(o => o.CourseID == CourseID) && program.Courses.First(o => o.CourseID == CourseID).CourseRecords.Any(o => o.PersonnelID == PersonID))
            {
                return program.Courses.First(o => o.CourseID == CourseID).CourseRecords.First(o => o.PersonnelID == PersonID).FinalPass;
            }
            return false;
        }
    }
}
