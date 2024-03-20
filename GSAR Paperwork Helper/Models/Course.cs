using GSAR_Paperwork_Helper.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSAR_Paperwork_Helper.Models
{
    public class Course
    {
        public Guid CourseID { get; set; } = Guid.Empty;
        public string? CourseName { get; set; }  
        public string? CourseCode { get; set; }
        public string? CourseLocation { get; set; }
        public string? SARGroupName { get; set; }
        public string? LeadInstructor {  get; set; }
        public List<string>? AssistantInstructors { get; set; } = new List<string>();
        public DateTime CourseStart { get; set; }
        public DateTime CourseEnd { get; set; }
        
        public int PracticalCount { get; set; }

        public List<CourseRecord> CourseRecords { get; set; } = new List<CourseRecord>();


        public Course()
        {CourseStart = DateTime.Now;
            CourseEnd = DateTime.Now;
            
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
            course.CourseName = "GSAR";
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
            course.CourseName = "Wilderness Survival for";
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
