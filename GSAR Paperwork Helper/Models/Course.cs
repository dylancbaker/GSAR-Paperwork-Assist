using GSAR_Paperwork_Helper.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GSAR_Paperwork_Helper.Models
{
    public class Course : INotifyPropertyChanged
    {
        private string? _CourseName;
        private string? _CourseCode;
        private string? _CourseLocation;
        private string? _SARGroupName;
        private string? _LeadInstructor;
        private string? _AssistantInstructors;
        private DateTime _CourseStart;
        private DateTime _CourseEnd;
        private int _PracticalCount;
        private BindingList<CourseRecord> _Records = new BindingList<CourseRecord>();


        public Guid CourseID { get; set; } = Guid.Empty;
        public string? CourseName { get => _CourseName; set { _CourseName = value; OnPropertyChanged(nameof(CourseName)); } }
        public string? CourseCode { get => _CourseCode; set { _CourseCode = value; OnPropertyChanged(nameof(CourseCode)); } }
        public string? CourseLocation { get => _CourseLocation; set { _CourseLocation = value; OnPropertyChanged(nameof(CourseLocation)); } }
        public string? SARGroupName { get => _SARGroupName; set { _SARGroupName = value; OnPropertyChanged(nameof(SARGroupName)); } }
        public string? LeadInstructor { get => _LeadInstructor; set { _LeadInstructor = value; OnPropertyChanged(nameof(LeadInstructor)); } }
        public string? AssistantInstructors { get => _AssistantInstructors; set { _AssistantInstructors = value; OnPropertyChanged(nameof(AssistantInstructors)); } }
        public DateTime CourseStart { get => _CourseStart; set { _CourseStart = value; OnPropertyChanged(nameof(CourseStart)); } }
        public DateTime CourseEnd { get => _CourseEnd; set { _CourseEnd = value; OnPropertyChanged(nameof(CourseEnd)); } }

        public int PracticalCount { get => _PracticalCount; set { _PracticalCount = value; OnPropertyChanged(nameof(PracticalCount)); } }
        public BindingList<CourseRecord> CourseRecords { get => _Records; set { _Records = value; OnPropertyChanged(nameof(CourseRecords)); } }

        public bool ShowPractical1 { get { if (PracticalCount > 0) { return true; } return false; } }
        public bool ShowPractical2 { get { if (PracticalCount > 1) { return true; } return false; } }
        public bool ShowPractical3 { get { if (PracticalCount > 2) { return true; } return false; } }
        public bool ShowPractical4 { get { if (PracticalCount > 3) { return true; } return false; } }
        
        
        public Course()
        {
            CourseStart = DateTime.Now;
            CourseEnd = DateTime.Now;
            LeadInstructor = Properties.Settings.Default.DefaultInstructor;
            CourseID = Guid.NewGuid();
            CourseRecords.ListChanged += CourseRecords_ListChanged;
        }

        private void CourseRecords_ListChanged(object? sender, ListChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CourseRecords));
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

    public static class CourseTools
    {
        public static List<Course> GetBlankCourses()
        {
            List<Course> courses = new List<Course>();

            courses.Add(GetEmrg1703());
            courses.Add(GetEmrg1701());
            courses.Add(GetEmrg1702());
            courses.Add(GetEmrg1704());
            courses.Add(GetEmrg1705());
            courses.Add(GetEmrg1706());
            courses.Add(GetEmrg1200());


            return courses;
        }

      
      
        public static Course GetEmrg1701()
        {
            Course course = new Course();
            course.CourseID = StaticValues.EMRG1701;
            course.CourseName = "GSAR Orientation & Safety";
            course.CourseCode = "EMRG-1701";
            course.PracticalCount = 1;
            return course;
        }
        public static Course GetEmrg1702()
        {
            Course course = new Course();
            course.CourseID = StaticValues.EMRG1702;
            course.CourseName = "Intro to Search Management";
            course.CourseCode = "EMRG-1702";
            course.PracticalCount = 2;
            return course;
        }
        public static Course GetEmrg1703()
        {
            Course course = new Course();
            course.CourseID = StaticValues.EMRG1703;
            course.CourseName = "Intro to SAR in BC";
            course.CourseCode = "EMRG-1703";
            course.PracticalCount = 0;
            return course;
        }
        public static Course GetEmrg1704()
        {
            Course course = new Course();
            course.CourseID = StaticValues.EMRG1704;
            course.CourseName = "Ground SAR Skills";
            course.CourseCode = "EMRG-1704";
            course.PracticalCount = 4;
            return course;
        }
        public static Course GetEmrg1705()
        {
            Course course = new Course();
            course.CourseID =StaticValues.EMRG1705;
            course.CourseName = "Navigation Skills";
            course.CourseCode = "EMRG-1705";
            course.PracticalCount = 1;
            return course;
        }
        public static Course GetEmrg1706()
        {
            Course course = new Course();
            course.CourseID = StaticValues.EMRG1706;
            course.CourseName = "Wilderness Survival for GSAR";
            course.CourseCode = "EMRG-1706";
            course.PracticalCount = 1;
            return course;
        }
        public static Course GetEmrg1200()
        {
            Course course = new Course();
            course.CourseID = StaticValues.EMRG1200;
            course.CourseName = "ICS 100";
            course.CourseCode = "EMRG-1200";
            course.PracticalCount = 0;
            return course;
        }
    }
}
