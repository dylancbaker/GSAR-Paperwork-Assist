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
        {
            
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
            course.CourseID = new Guid("b6237597-e33c-4e67-a77a-83fadd9ba043");
            course.CourseName = "GSAR Orientation & Safety";
            course.CourseCode = "EMRG-1701";
            course.PracticalCount = 1;
            return course;
        }
        public static Course GetEmrg1702()
        {
            Course course = new Course();
            course.CourseID = new Guid("68fe5ae3-1c63-4ad4-a002-4fc04c967d87");
            course.CourseName = "Intro to Search Management";
            course.CourseCode = "EMRG-1702";
            course.PracticalCount = 2;
            return course;
        }
        public static Course GetEmrg1703()
        {
            Course course = new Course();
            course.CourseID = new Guid("00c4ee2d-25b9-49f5-803a-5d7b33e9b746");
            course.CourseName = "Intro to SAR in BC";
            course.CourseCode = "EMRG-1703";
            course.PracticalCount = 0;
            return course;
        }
        public static Course GetEmrg1704()
        {
            Course course = new Course();
            course.CourseID = new Guid("04d0fa1e-bce6-4502-a951-430c16948e08");
            course.CourseName = "GSAR";
            course.CourseCode = "EMRG-1704";
            course.PracticalCount = 4;
            return course;
        }
        public static Course GetEmrg1705()
        {
            Course course = new Course();
            course.CourseID = new Guid("d92e5b95-795d-4561-a020-af02d0bcf38d");
            course.CourseName = "Navigation Skills";
            course.CourseCode = "EMRG-1705";
            course.PracticalCount = 1;
            return course;
        }
        public static Course GetEmrg1706()
        {
            Course course = new Course();
            course.CourseID = new Guid("87f2df40-cdaf-48d1-abf0-b2b5a0ad5220");
            course.CourseName = "Wilderness Survival for";
            course.CourseCode = "EMRG-1706";
            course.PracticalCount = 1;
            return course;
        }
        public static Course GetEmrg1200()
        {
            Course course = new Course();
            course.CourseID = new Guid("5b65ac66-5611-4bb3-a78a-9ecc919b5b79");
            course.CourseName = "ICS 100";
            course.CourseCode = "EMRG-1200";
            course.PracticalCount = 0;
            return course;
        }
    }
}
